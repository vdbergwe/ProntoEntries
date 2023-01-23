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
            If ModelState.IsValid Then
                db.Entries.Add(entry)
                db.SaveChanges()
                Result = True
            End If
            SendConfirmationLink(entry.ParticipantID, entry.EntryID)
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
            msg.Subject = "Entry Confirmation:" + EventName.ToString()

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
