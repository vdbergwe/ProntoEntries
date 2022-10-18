Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries

Namespace Controllers
    Public Class EntriesController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities


        ' GET: Entries/Cart
        Function Cart(ByVal id As Integer?, ByVal DivisionSelect As Integer?) As ActionResult

            Dim CartContent = db.Entries.Where(Function(a) a.Status = "UnPaid")
            ViewBag.Total = db.Entries.Where(Function(a) a.Status = "UnPaid").Sum(Function(b) b.Amount)
            'Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            'Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            'ViewBag.DivisionSelect = DivisionSelect
            'ViewBag.DivisionID = New SelectList(db.Divisions.Where(Function(a) a.RaceID = id), "DivisionID", "Category", DivisionSelect)
            'ViewBag.RaceID = id
            'ViewBag.RaceName = raceEvent.RaceName
            'ViewBag.Background = raceEvent.Background
            'ViewBag.OrgID = Organiser.OrgName
            'ViewBag.OrgImage = Organiser.Image
            'ViewBag.RaceDate = raceEvent.RaceDate.Value.ToString("dddd, dd MMMM yyyy")
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
            entry.ParticipantID = id
            entry.RaceID = RaceID
            entry.DivisionID = DivisionID
            entry.Amount = db.Divisions.Where(Function(a) a.DivisionID = DivisionID).Select(Function(a) a.Price).FirstOrDefault
            entry.Status = "UnPaid"
            If ModelState.IsValid Then
                db.Entries.Add(entry)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(entry)
        End Function

        ' GET: Entries
        Function Index() As ActionResult
            Return View(db.Entries.ToList())
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
