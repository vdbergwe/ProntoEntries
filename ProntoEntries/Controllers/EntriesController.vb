Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries
Imports System.Security.Cryptography


Namespace Controllers
    Public Class EntriesController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' POST: Entries/ConfirmPayment
        Function ConfirmPayment(ByVal paymentdata As ITN_Payload, ByVal pftrans As pflog, ByVal entry As Entry) As ActionResult

            If db.pflogs.Where(Function(a) a.Pf_reference = paymentdata.pf_payment_id And a.Pf_status = "COMPLETE").Count() = 0 Then

                pftrans.Pf_status = paymentdata.payment_status
                pftrans.M_reference = paymentdata.m_payment_id
                pftrans.Pf_reference = paymentdata.pf_payment_id
                pftrans.Merchant_id = paymentdata.merchant_id
                pftrans.amount_gross = paymentdata.amount_gross
                pftrans.amount_fee = paymentdata.amount_fee
                pftrans.amount_net = paymentdata.amount_net
                db.pflogs.Add(pftrans)
                db.SaveChanges()


                Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = paymentdata.custom_str1 And a.RaceID IsNot Nothing).FirstOrDefault()
                Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = paymentdata.custom_str1)
                Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
                Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()
                Dim Total = 0.00
                For Each sale In Transaction
                    If sale.OptionID Is Nothing Then
                        Total += db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                    Else
                        Total += db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                    End If
                Next

                If (paymentdata.amount_gross = Total) Then
                    Dim MD5String = "m_payment_id=" + System.Net.WebUtility.UrlEncode(paymentdata.m_payment_id) _
                                + "&pf_payment_id=" + System.Net.WebUtility.UrlEncode(paymentdata.pf_payment_id) _
                                + "&payment_status=" + System.Net.WebUtility.UrlEncode(paymentdata.payment_status) _
                                + "&item_name=" + System.Net.WebUtility.UrlEncode(paymentdata.item_name) _
                                + "&item_description=" + System.Net.WebUtility.UrlEncode(paymentdata.item_description) _
                                + "&amount_gross=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_gross) _
                                + "&amount_fee=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_fee) _
                                + "&amount_net=" + System.Net.WebUtility.UrlEncode(paymentdata.amount_net) _
                                + "&custom_str1=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str1) _
                                + "&custom_str2=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str2) _
                                + "&custom_str3=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str3) _
                                + "&custom_str4=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str4) _
                                + "&custom_str5=" + System.Net.WebUtility.UrlEncode(paymentdata.custom_str5) _
                                + "&custom_int1=" _
                                + "&custom_int2=" _
                                + "&custom_int3=" _
                                + "&custom_int4=" _
                                + "&custom_int5=" _
                                + "&name_first=" + System.Net.WebUtility.UrlEncode(paymentdata.name_first) _
                                + "&name_last=" + System.Net.WebUtility.UrlEncode(paymentdata.name_last) _
                                + "&email_address=" + System.Net.WebUtility.UrlEncode(paymentdata.email_address) _
                                + "&merchant_id=" + System.Net.WebUtility.UrlEncode(paymentdata.merchant_id) _
                                + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

                    Dim md5 As MD5 = MD5.Create()
                    Dim Bytes As Byte() = Encoding.ASCII.GetBytes(MD5String)
                    Dim hash As Byte() = md5.ComputeHash(Bytes)
                    Dim sBuilder As New StringBuilder()
                    For i As Integer = 0 To hash.Length - 1
                        sBuilder.Append(hash(i).ToString("x2"))
                    Next

                    If (paymentdata.signature = sBuilder.ToString()) Then
                        Dim result As Boolean
                        Dim SControl As New Controllers.SalesController()
                        Dim AllSales = db.Sales.Where(Function(a) a.M_reference = paymentdata.m_payment_id And a.Pf_reference Is Nothing)
                        Dim AllParticipantSales = db.Sales.Where(Function(a) a.M_reference = paymentdata.m_payment_id And a.RaceID IsNot Nothing And a.Pf_reference Is Nothing)

                        For Each sale In AllParticipantSales
                            entry.ParticipantID = sale.ParticipantID
                            entry.RaceID = sale.RaceID
                            entry.DivisionID = sale.DivisionID
                            entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = entry.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                            entry.Status = "Paid"
                            entry.PaymentReference = paymentdata.m_payment_id.ToString()
                            entry.MainUserID = paymentdata.custom_str1
                            entry.PayFastReference = paymentdata.pf_payment_id.ToString()
                            entry.PayFastStatus = paymentdata.payment_status
                            result = SControl.UpdateEntries(entry)
                        Next


                        For Each sale In AllSales
                            sale.Pf_reference = paymentdata.pf_payment_id
                            sale.Verified = 1
                            result = SControl.UpdateSales(sale)
                        Next

                        If result Then
                            Return New HttpStatusCodeResult(HttpStatusCode.OK)
                        End If

                        Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                    Else
                        Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                    End If
                Else
                    Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)
                End If
            End If
            Return New HttpStatusCodeResult(HttpStatusCode.Unauthorized)


        End Function

        Function SubmitToPayfast() As ActionResult
            Dim SingleTransaction As Sale = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name And a.RaceID IsNot Nothing).FirstOrDefault()
            Dim Transaction = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name)
            Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
            Dim OrgPassphrase = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantPassPhrase).FirstOrDefault()
            Dim hosturl = "https://3c61-197-245-9-201.in.ngrok.io"

            ViewBag.Total = 0
            For Each sale In Transaction
                If sale.OptionID Is Nothing Then
                    ViewBag.Total = ViewBag.Total + db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                Else
                    ViewBag.Total = ViewBag.Total + db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                End If
            Next

            ViewBag.MReference = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).Select(Function(b) b.M_reference).FirstOrDefault()
            ViewBag.EmailAddress = db.Participants.Where(Function(a) a.EmailAddress = User.Identity.Name).Select(Function(b) b.EmailAddress).FirstOrDefault()
            ViewBag.Emailconfirmation = "1"
            ViewBag.MerchantID = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantID).FirstOrDefault()
            ViewBag.Merchant_key = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantKey).FirstOrDefault()
            ViewBag.ReturnURL = hosturl + "/Entries/Index"
            ViewBag.CancelURL = hosturl + "/Entries/Cart"
            ViewBag.NotifyURL = hosturl + "/Entries/Confirmpayment"
            ViewBag.item_name = db.RaceEvents.Where(Function(a) a.RaceID = SingleTransaction.RaceID).Select(Function(b) b.RaceName).FirstOrDefault()
            ViewBag.Amount = ViewBag.Total

            Dim TransactionString = "merchant_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MerchantID) + "&merchant_key=" + System.Net.WebUtility.UrlEncode(ViewBag.Merchant_key) _
                 + "&return_url=" + System.Net.WebUtility.UrlEncode(ViewBag.ReturnURL) + "&cancel_url=" + System.Net.WebUtility.UrlEncode(ViewBag.CancelURL) _
                 + "&notify_url=" + System.Net.WebUtility.UrlEncode(ViewBag.NotifyURL) + "&m_payment_id=" + System.Net.WebUtility.UrlEncode(ViewBag.MReference) _
                 + "&amount=" + System.Net.WebUtility.UrlEncode(ViewBag.Amount) _
                 + "&item_name=" + System.Net.WebUtility.UrlEncode(ViewBag.item_name.ToString) + "&custom_str1=" _
                 + System.Net.WebUtility.UrlEncode(ViewBag.EmailAddress)

            Dim Constring = TransactionString + "&passphrase=" + System.Net.WebUtility.UrlEncode(OrgPassphrase)

            Dim md5 As MD5 = MD5.Create()
            Dim Bytes As Byte() = Encoding.ASCII.GetBytes(Constring)
            Dim hash As Byte() = md5.ComputeHash(Bytes)
            Dim sBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                sBuilder.Append(hash(i).ToString("x2"))
            Next

            ViewBag.Signature = sBuilder.ToString

            TransactionString = TransactionString + "&" + "signature=" + ViewBag.Signature

            Return Redirect("https://sandbox.payfast.co.za/eng/process?" + TransactionString)
        End Function

        ' GET: Entries/Cart
        Function Cart(ByVal id As Integer?, ByVal DivisionSelect As Integer?) As ActionResult
            Dim CartContent = db.Sales.Where(Function(a) a.Pf_reference Is Nothing And a.UserID = User.Identity.Name).OrderBy(Function(b) b.ParticipantID).ThenByDescending(Function(b) b.RaceID)
            ViewBag.Total = 0
            For Each sale In CartContent
                If sale.OptionID Is Nothing Then
                    ViewBag.Total = ViewBag.Total + db.Divisions.Where(Function(a) a.DivisionID = sale.DivisionID).Select(Function(b) b.Price).FirstOrDefault()
                Else
                    ViewBag.Total = ViewBag.Total + db.AddonOptions.Where(Function(a) a.OptionID = sale.OptionID).Select(Function(b) b.Amount).FirstOrDefault()
                End If
            Next

            Return View(CartContent.ToList())
        End Function


        ' GET: Entries/Create
        Function NewEntry(ByVal id As Integer?, ByVal DivisionSelect As Integer?) As ActionResult
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgImage = Organiser.Image
            ViewBag.DivisionSelect = DivisionSelect
            ViewBag.DivisionID = New SelectList(db.Divisions.Where(Function(a) a.RaceID = id), "DivisionID", "Category", DivisionSelect)
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgImage = Organiser.Image
            ViewBag.RaceID = id
            ViewBag.RaceName = raceEvent.RaceName
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgID = Organiser.OrgName
            ViewBag.OrgImage = Organiser.Image
            ViewBag.RaceDate = raceEvent.RaceDate.Value.ToString("dddd, dd MMMM yyyy")
            Return View()
        End Function

        Function Addtocart(ByVal sale As Sale, ByVal id As Integer?, ByVal RaceID As Integer?, ByVal DivisionID As Integer?) As ActionResult
            Dim OrderNumber As Integer
            sale.ParticipantID = id
            sale.RaceID = RaceID
            sale.DivisionID = DivisionID
            Dim Amount = db.Divisions.Where(Function(a) a.DivisionID = DivisionID).Select(Function(a) a.Price).FirstOrDefault
            Dim Status = "UnPaid"
            sale.UserID = User.Identity.Name
            sale.TandC = True
            sale.Indemnity = True

            If (db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Count() > 0) Then
                sale.M_reference = db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Select(Function(a) a.M_reference).FirstOrDefault()
            Else
                If IsNothing(db.Sales.Max(Function(a) a.M_reference)) Then
                    sale.M_reference = 1
                Else
                    OrderNumber = db.Sales.Max(Function(a) a.M_reference)
                    sale.M_reference = OrderNumber + 1
                End If
            End If
            If ModelState.IsValid Then
                db.Sales.Add(sale)
                db.SaveChanges()
                '@Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID}, New With {.class = "btnEntryLink"})
                Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID})
            End If
            Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID})
        End Function

        Function VerifyEntry(ByVal sale As Sale, ByVal Id As Integer?, ByVal RaceID1 As Integer?, ByVal DivisionID1 As Integer?, ByVal OptionID1 As Integer?, ByVal ItemID As Integer?) As ActionResult
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(RaceID1)
            Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            Dim OrderNumber As Integer

            ViewBag.Background = raceEvent.Background
            ViewBag.OrgImage = Organiser.Image

            ViewBag.Indemnity = db.RaceEvents.Where(Function(a) a.RaceID = RaceID1).Select(Function(b) b.Indemnity).FirstOrDefault()
            ViewBag.TandC = db.RaceEvents.Where(Function(a) a.RaceID = RaceID1).Select(Function(b) b.TandC).FirstOrDefault()
            ViewBag.Racename = db.RaceEvents.Where(Function(a) a.RaceID = RaceID1).Select(Function(b) b.RaceName).FirstOrDefault()
            ViewBag.ParticipantID = Id
            ViewBag.RaceID = RaceID1
            ViewBag.DivisionID = DivisionID1
            ViewBag.OptionID = OptionID1
            ViewBag.ItemID = ItemID

            Dim holding = db.Sales.Where(Function(a) a.ParticipantID = Id And a.Pf_reference Is Nothing)
            Dim AddItems = db.AddonItems.Where(Function(a) a.RaceID = RaceID1 And Not holding.Any(Function(b) b.ItemID = a.ItemID))

            If (db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Count() > 0) Then
                sale.M_reference = db.Sales.Where(Function(a) a.UserID = User.Identity.Name And a.Pf_reference Is Nothing).Select(Function(a) a.M_reference).FirstOrDefault()
            Else
                If IsNothing(db.Sales.Max(Function(a) a.M_reference)) Then
                    sale.M_reference = 1
                Else
                    OrderNumber = db.Sales.Max(Function(a) a.M_reference)
                    sale.M_reference = OrderNumber + 1
                End If
            End If

            If OptionID1 IsNot Nothing Then
                sale.ParticipantID = Id
                sale.ItemID = ItemID
                sale.OptionID = OptionID1
                sale.UserID = User.Identity.Name
                If ModelState.IsValid Then
                    db.Sales.Add(sale)
                    db.SaveChanges()
                    Return RedirectToAction("VerifyEntry", "Entries", New With {.id = Id, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID})
                End If
                ViewBag.OptionID = Nothing
            End If

            'var result = remoteProducts.Where(p=> !localProducts.Any(x=>x.matnum == p.ProductId)).ToList();


            'Dim OrderNumber As Integer
            'Entry.ParticipantID = id
            'Entry.RaceID = RaceID
            'Entry.DivisionID = DivisionID
            'Entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = DivisionID).Select(Function(a) a.Price).FirstOrDefault
            'Entry.Status = "UnPaid"
            'Entry.MainUserID = User.Identity.Name

            'If (db.Entries.Where(Function(a) a.MainUserID = User.Identity.Name And a.Status = "UnPaid").Count() > 0) Then
            '    Entry.PaymentReference = db.Entries.Where(Function(a) a.MainUserID = User.Identity.Name And a.Status = "UnPaid").Select(Function(a) a.PaymentReference).FirstOrDefault()
            'Else
            '    If IsNothing(db.Entries.Max(Function(a) a.PaymentReference)) Then
            '        Entry.PaymentReference = 1
            '    Else
            '        OrderNumber = db.Entries.Max(Function(a) a.PaymentReference)
            '        Entry.PaymentReference = OrderNumber + 1
            '    End If
            'End If
            'If ModelState.IsValid Then
            '    db.Entries.Add(Entry)
            '    db.SaveChanges()
            '    '@Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID}, New With {.class = "btnEntryLink"})
            '    Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID})
            'End If

            Return View(AddItems.ToList())
        End Function

        Function get_AddonOptionlist(Id As Integer?, ByVal RaceID As Integer?, ByVal DivisionID1 As Integer?, ByVal OptionID As Integer?, ByVal ParticipantID As Integer?) As ActionResult
            Dim Optionlist = db.AddonOptions.Where(Function(a) a.ItemID = Id)
            ViewBag.ParticipantID = ParticipantID
            ViewBag.RaceID = RaceID
            ViewBag.DivisionID = DivisionID1
            ViewBag.OptionID = OptionID
            ViewBag.ItemID = Id

            Return PartialView(Optionlist.ToList())
        End Function

        ' GET: Entries
        Function Index() As ActionResult
            Dim EntriesContent = db.Entries.Where(Function(a) a.Status <> "UnPaid")
            Return View(EntriesContent.ToList())
        End Function

        ' GET: Entries/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' GET: Entries/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Entries/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="EntryID,ParticipantID,RaceID,DivisionID,Amount,Status,PaymentReference,DistanceChange,ChangePaymentRef,TransferID,Result")> ByVal entry As Entry) As ActionResult
            If ModelState.IsValid Then
                db.Entries.Add(entry)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(entry)
        End Function

        ' GET: Entries/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' POST: Entries/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="EntryID,ParticipantID,RaceID,DivisionID,Amount,Status,PaymentReference,DistanceChange,ChangePaymentRef,TransferID,Result")> ByVal entry As Entry) As ActionResult
            If ModelState.IsValid Then
                db.Entry(entry).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(entry)
        End Function

        ' GET: Entries/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim entry As Entry = db.Entries.Find(id)
            If IsNothing(entry) Then
                Return HttpNotFound()
            End If
            Return View(entry)
        End Function

        ' POST: Entries/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim entry As Entry = db.Entries.Find(id)
            db.Entries.Remove(entry)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
