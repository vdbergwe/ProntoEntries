Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Configuration

Namespace Controllers
    Public Class SalesController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' POST: Sales/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function UpdateSales(<Bind(Include:="SaleID,RaceID,DivisionID,ItemID,UserID,Indemnity,TandC,ParticipantID,M_reference,Pf_reference,OptionID,Verified")> ByVal sale As Sale)
            Dim result As Boolean
            If ModelState.IsValid Then
                db.Entry(sale).State = EntityState.Modified
                db.SaveChanges()
                result = True
            End If

            Return (result)
        End Function

        ' POST: Entries/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function UpdateEntries(<Bind(Include:="EntryID,ParticipantID,RaceID,DivisionID,Amount,Status,PaymentReference,DistanceChange,ChangePaymentRef,TransferID,Result")> ByVal entry As Entry)
            Dim Result As Boolean
            Dim logText As New SystemLog

            logText.UserID = entry.ParticipantID
            logText.Date = Now()
            logText.Time = Now().TimeOfDay()

            Result = False
            If ModelState.IsValid Then
                db.Entries.Add(entry)
                db.SaveChanges()
                logText.ActionPerformed = "Entry: " + entry.MainUserID.ToString() + " - " + entry.RaceID.ToString()
                If ModelState.IsValid Then
                    db.SystemLogs.Add(logText)
                    db.SaveChanges()
                End If
                Result = True
            End If
            Try
                'Code that might throw an exception goes here
                SendConfirmationLink(entry.ParticipantID, entry.EntryID)
                logText.ActionPerformed = "Email Sent Good"
            Catch ex As Exception
                'Exception handling code goes here
                logText.ActionPerformed = ex.ToString()

                If ModelState.IsValid Then
                    db.SystemLogs.Add(logText)
                    db.SaveChanges()
                Else
                    logText.ActionPerformed = "Ex String too Long"
                    If ModelState.IsValid Then
                        db.SystemLogs.Add(logText)
                        db.SaveChanges()
                    End If
                End If

                Return Result
            End Try

            db.SystemLogs.Add(logText)
            db.SaveChanges()
            Return Result
        End Function

        Function SendConfirmationLink(ParticipantID As Integer?, EntryID As Integer?)
            Dim Html As String

            Dim Email = db.Participants.Where(Function(a) a.ParticipantID = ParticipantID).Select(Function(b) b.EmailAddress).FirstOrDefault()
            Dim RaceID = db.Entries.Where(Function(a) a.EntryID = EntryID).Select(Function(b) b.RaceID).FirstOrDefault()
            Dim EventName = db.RaceEvents.Where(Function(a) a.RaceID = RaceID).Select(Function(b) b.RaceName).FirstOrDefault()

            Dim link As String
            link = "https://entries.prontocs.co.za/Entries/GenerateTicket/" + EntryID.ToString()

            Html = "You have successfully entered: " + EventName.ToString() + ". <br/><br/>"

            Html += HttpUtility.HtmlEncode("Your confirmation can be viewed and printed at: " + link)


            Dim msg As New MailMessage With {
            .From = New MailAddress(ConfigurationManager.AppSettings("Email").ToString())
            }
            msg.To.Add(New MailAddress(Email))
            msg.Bcc.Add(New MailAddress("vdbergwe@gmail.com"))
            msg.Subject = "Entry Confirmation: " + EventName.ToString()

            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(Html, Nothing, MediaTypeNames.Text.Html))

            Dim smtpClient As New SmtpClient("prontocs.co.za", Convert.ToInt32(587))
            Dim credentials As New System.Net.NetworkCredential(ConfigurationManager.AppSettings("Email").ToString(), ConfigurationManager.AppSettings("Password").ToString())
            smtpClient.Credentials = credentials
            smtpClient.EnableSsl = True
            smtpClient.Send(msg)

            Return True
        End Function

        ' GET: Sales
        <Authorize>
        Function Index() As ActionResult
            Return View(db.Sales.ToList())
        End Function

        Function Get_ItemImage(ByVal id As Integer?) As ActionResult
            ViewBag.Image = db.RaceEvents.Where(Function(a) a.RaceID = id).Select(Function(b) b.Image).FirstOrDefault()

            Return PartialView()
        End Function

        Function Get_ItemSize(ByVal id As Integer?) As ActionResult
            ViewBag.Size = New SelectList(db.AddonOptions.Where(Function(a) a.ItemID = id), "OptionID", "Size")

            Return PartialView()
        End Function

        ' GET: Shop
        <Authorize>
        Function Shop(ByVal Size As Integer?, ByVal ParticipantID As Integer?, ByVal Sale As Sale) As ActionResult
            Dim AllOptions = db.AddonOptions.Where(Function(a) a.StopDate > Now())
            Dim Items = db.AddonItems.Where(Function(a) AllOptions.Any(Function(c) c.ItemID = a.ItemID) And a.AllowedInShop = True)
            ViewBag.ParticipantID = New SelectList(db.Participants.Where(Function(a) a.UserID = User.Identity.Name), "ParticipantID", "FirstName")

            Dim ItemID = db.AddonOptions.Where(Function(a) a.OptionID = Size).Select(Function(b) b.ItemID).FirstOrDefault()
            Dim AllowFlag = True

            ViewBag.HasItem = False
            If db.Sales.Where(Function(a) a.ParticipantID = ParticipantID And a.ItemID = ItemID And a.Pf_reference IsNot Nothing).Count() > 0 Then
                ViewBag.HasItem = True
            End If

            ViewBag.ItemInCart = False
            Dim CheckDB = db.Sales.Where(Function(a) a.ItemID = ItemID And a.ParticipantID = ParticipantID And a.UserID = User.Identity.Name).Count()

            If CheckDB > 0 Then
                ViewBag.ItemInCart = True
            End If

            ViewBag.NoEntry = True
            If ParticipantID Is Nothing Then
                ViewBag.NoEntry = False
            End If

            If db.Entries.Where(Function(a) a.ParticipantID = ParticipantID And a.RaceID = db.AddonItems.Where(Function(b) b.ItemID = ItemID).Select(Function(c) c.RaceID).FirstOrDefault()).Count() = 1 Then
                ViewBag.NoEntry = False
            End If

            If ViewBag.HasItem = True Or ViewBag.ItemInCart = True Or ViewBag.NoEntry = True Then
                AllowFlag = False
            End If

            If AllowFlag = True And Size IsNot Nothing And ParticipantID IsNot Nothing Then
                Dim RaceID = db.AddonItems.Where(Function(a) a.ItemID = ItemID).Select(Function(b) b.RaceID).FirstOrDefault()
                Sale.ItemID = ItemID
                Sale.OptionID = Size
                Sale.UserID = User.Identity.Name
                Sale.ParticipantID = ParticipantID
                Sale.M_reference = db.Sales.Where(Function(a) a.ParticipantID = ParticipantID And a.RaceID = RaceID).Select(Function(b) b.M_reference).FirstOrDefault()
                Sale.SaleDate = Now()

                If ModelState.IsValid Then
                    db.Sales.Add(Sale)
                    db.SaveChanges()
                    'Return RedirectToAction("VerifyEntry", "Entries", New With {.id = Id, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID})
                End If
            End If

            Return View(Items.ToList())
        End Function

        ' GET: Sales/Details/5
        <Authorize>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim sale As Sale = db.Sales.Find(id)
            If IsNothing(sale) Then
                Return HttpNotFound()
            End If
            Return View(sale)
        End Function

        ' GET: Sales/Create
        <Authorize>
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Sales/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Create(<Bind(Include:="SaleID,RaceID,DivisionID,ItemID,UserID,Indemnity,TandC,ParticipantID,M_reference,Pf_reference,OptionID,Verified")> ByVal sale As Sale) As ActionResult
            If ModelState.IsValid Then
                db.Sales.Add(sale)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(sale)
        End Function

        ' GET: Sales/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim sale As Sale = db.Sales.Find(id)
            If IsNothing(sale) Then
                Return HttpNotFound()
            End If
            Return View(sale)
        End Function

        ' POST: Sales/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Edit(<Bind(Include:="SaleID,RaceID,DivisionID,ItemID,UserID,Indemnity,TandC,ParticipantID,M_reference,Pf_reference,OptionID,Verified")> ByVal sale As Sale) As ActionResult
            If ModelState.IsValid Then
                db.Entry(sale).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(sale)
        End Function

        ' GET: Sales/Delete/5
        <Authorize>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim sale As Sale = db.Sales.Find(id)
            If IsNothing(sale) Then
                Return HttpNotFound()
            End If
            Return View(sale)
        End Function

        ' POST: Sales/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim sale As Sale = db.Sales.Find(id)
            Dim removesales = db.Sales.Where(Function(a) a.M_reference = sale.M_reference And a.ParticipantID = sale.ParticipantID).ToList()
            For Each sale In removesales
                db.Sales.Remove(sale)
                db.SaveChanges()
            Next
            Return RedirectToAction("Cart", "Entries")

        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
