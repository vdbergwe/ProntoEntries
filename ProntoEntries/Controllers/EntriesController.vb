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

        ' GET: Entries/Cart
        Function Cart(ByVal id As Integer?, ByVal DivisionSelect As Integer?) As ActionResult
            Dim CartContent = db.Entries.Where(Function(a) a.Status = "UnPaid" And a.MainUserID = User.Identity.Name)
            ViewBag.Total = db.Entries.Where(Function(a) a.Status = "UnPaid" And a.MainUserID = User.Identity.Name).Sum(Function(b) b.Amount)
            ViewBag.PaymentReference = db.Entries.Where(Function(a) a.Status = "UnPaid" And a.MainUserID = User.Identity.Name).Select(Function(b) b.PaymentReference).FirstOrDefault()

            Dim Transaction As Entry = db.Entries.Where(Function(a) a.Status = "UnPaid" And a.MainUserID = User.Identity.Name).FirstOrDefault()
            Dim OrgID = db.RaceEvents.Where(Function(a) a.RaceID = Transaction.RaceID).Select(Function(b) b.OrgID).FirstOrDefault()
            ViewBag.EmailAddress = db.Participants.Where(Function(a) a.EmailAddress = User.Identity.Name).Select(Function(b) b.EmailAddress).FirstOrDefault().ToUpper
            ViewBag.Emailconfirmation = "1"
            ViewBag.MerchantID = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantID).FirstOrDefault().ToUpper()
            ViewBag.Merchant_key = db.PaymentDetails.Where(Function(a) a.OrgID = OrgID).Select(Function(b) b.MerchantKey).FirstOrDefault().ToUpper()
            ViewBag.ReturnURL = "https://www.google.co.za".ToUpper()
            ViewBag.CancelURL = "https://www.facebook.com".ToUpper()
            ViewBag.NotifyURL = "https://www.google.co.za".ToUpper()
            ViewBag.item_name = db.RaceEvents.Where(Function(a) a.RaceID = Transaction.RaceID).Select(Function(b) b.RaceName).FirstOrDefault().ToUpper()
            ViewBag.item_name = Replace(ViewBag.item_name, " ", "+")
            ViewBag.Amount = db.Entries.Where(Function(a) a.Status = "UnPaid" And a.MainUserID = User.Identity.Name).Sum(Function(b) b.Amount).ToString().ToUpper()
            Dim Constring = "merchant_id=" + ViewBag.MerchantID + "&merchant_key=" + ViewBag.Merchant_key + "&return_url=" + ViewBag.ReturnURL + "&cancel_url=" + ViewBag.CancelURL + "&notify_url=" + ViewBag.NotifyURL + "&amount=" + ViewBag.Amount + "&item_name=" + ViewBag.item_name.ToString + "&confirmation_address=" + ViewBag.EmailAddress + "&passphrase=Citybugnelspruit1"
            Constring = Replace(Constring.ToString(), " ", "+")
            Dim md5 As MD5 = MD5.Create()
            Dim Bytes As Byte() = Encoding.ASCII.GetBytes(Constring)
            Dim hash As Byte() = md5.ComputeHash(Bytes)
            Dim sBuilder As New StringBuilder()
            For i As Integer = 0 To hash.Length - 1
                sBuilder.Append(hash(i).ToString("x2"))
            Next


            ViewBag.Signature = sBuilder.ToString
            'ViewBag.Signature = "klompniks"


            Return View(CartContent.ToList())
        End Function


        ' GET: Entries/Create
        Function NewEntry(ByVal id As Integer?, ByVal DivisionSelect As Integer?) As ActionResult
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            ViewBag.DivisionSelect = DivisionSelect
            ViewBag.DivisionID = New SelectList(db.Divisions.Where(Function(a) a.RaceID = id), "DivisionID", "Category", DivisionSelect)
            ViewBag.RaceID = id
            ViewBag.RaceName = raceEvent.RaceName
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgID = Organiser.OrgName
            ViewBag.OrgImage = Organiser.Image
            ViewBag.RaceDate = raceEvent.RaceDate.Value.ToString("dddd, dd MMMM yyyy")
            Return View()
        End Function

        Function Addtocart(<Bind(Include:="EntryID,ParticipantID,RaceID,DivisionID,Amount,Status,PaymentReference,DistanceChange,ChangePaymentRef,TransferID,Result")> ByVal entry As Entry, ByVal id As Integer?, ByVal RaceID As Integer?, ByVal DivisionID As Integer?) As ActionResult
            Dim OrderNumber As Integer
            entry.ParticipantID = id
            entry.RaceID = RaceID
            entry.DivisionID = DivisionID
            entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = DivisionID).Select(Function(a) a.Price).FirstOrDefault
            entry.Status = "UnPaid"
            entry.MainUserID = User.Identity.Name

            If (db.Entries.Where(Function(a) a.MainUserID = User.Identity.Name And a.Status = "UnPaid").Count() > 0) Then
                entry.PaymentReference = db.Entries.Where(Function(a) a.MainUserID = User.Identity.Name And a.Status = "UnPaid").Select(Function(a) a.PaymentReference).FirstOrDefault()
            Else
                If IsNothing(db.Entries.Max(Function(a) a.PaymentReference)) Then
                    entry.PaymentReference = 1
                Else
                    OrderNumber = db.Entries.Max(Function(a) a.PaymentReference)
                    entry.PaymentReference = OrderNumber + 1
                End If
            End If
                If ModelState.IsValid Then
                db.Entries.Add(entry)
                db.SaveChanges()
                '@Html.ActionLink("Enter Now", "NewEntry", "Entries", New With {.id = Model.RaceID}, New With {.class = "btnEntryLink"})
                Return RedirectToAction("NewEntry", "Entries", New With {.id = RaceID, .DivisionSelect = DivisionID})
            End If
            Return View(entry)
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
