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
    Public Class ParticipantsController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities


        Function Get_Age(Id As Integer?, RaceID As Integer?) As ActionResult
            Dim RaceDate As Date = db.RaceEvents.Where(Function(c) c.RaceID = RaceID).Select(Function(d) d.RaceDate).FirstOrDefault()
            Dim ParticipantDOB As Date = db.Participants.Where(Function(a) a.ParticipantID = Id).Select(Function(b) b.DOB).FirstOrDefault()
            Dim Age As TimeSpan = RaceDate - ParticipantDOB
            Dim AgeInt As Decimal = Age.TotalDays / 365

            ViewBag.GetAge = Math.Round(AgeInt, 0)
            Dim DivisionID = db.Divisions.Where(Function(a) a.RaceID = RaceID And a.MinAge < AgeInt And a.MaxAge > AgeInt).Select(Function(b) b.DivisionID).FirstOrDefault()
            Return PartialView()
        End Function

        ' GET: Entries/Details/5
        <Authorize>
        Function ViewParticipants(ByVal id As Integer?, ByVal DivisionSelect As Decimal?) As ActionResult
            ViewBag.RaceID = id
            ViewBag.DivisionSelect = DivisionSelect
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If

            Dim Participant = db.Participants.Where(Function(a) a.UserID = User.Identity.Name And (db.Entries.Where(Function(b) b.RaceID = id And b.ParticipantID = a.ParticipantID).Count() = 0 _
                                                         And db.Sales.Where(Function(b) b.RaceID = id And b.ParticipantID = a.ParticipantID).Count() = 0)).ToList()


            'Dim Participant = db.Participants.ToList()
            If IsNothing(Participant) Then
                Return HttpNotFound()
            End If
            Return PartialView(Participant)
        End Function

        ' GET: Participants
        <Authorize>
        Function Index() As ActionResult
            Dim Participant = db.Participants.Where(Function(a) a.UserID = User.Identity.Name)
            Return View(Participant.ToList())
        End Function

        ' GET: Participants/Details/5
        <Authorize>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim participant As Participant = db.Participants.Find(id)
            If IsNothing(participant) Then
                Return HttpNotFound()
            End If
            Return View(participant)
        End Function

        ' GET: Participants/Create
        <Authorize>
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Participants/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Create(<Bind(Include:="ParticipantID,FirstName,MiddleNames,LastName,IDNumber,DOB,RaceNumber,EmailAddress,MedicalName,MedicalNumber,EmergencyContact,EmergencyNumber,BoodType,Allergies,AdditionalInfo,DoctorName,DoctorContact,Clubname,Country,Address,City,Province,UserID,EventMailer,Offers")> ByVal participant As Participant) As ActionResult
            participant.UserID = User.Identity.Name
            If ModelState.IsValid Then
                db.Participants.Add(participant)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(participant)
        End Function

        ' GET: Participants/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim participant As Participant = db.Participants.Find(id)
            If IsNothing(participant) Then
                Return HttpNotFound()
            End If
            Return View(participant)
        End Function

        ' POST: Participants/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Edit(<Bind(Include:="ParticipantID,FirstName,MiddleNames,LastName,IDNumber,DOB,RaceNumber,EmailAddress,MedicalName,MedicalNumber,EmergencyContact,EmergencyNumber,BoodType,Allergies,AdditionalInfo,DoctorName,DoctorContact,Clubname,Country,Address,City,Province,UserID,EventMailer,Offers")> ByVal participant As Participant) As ActionResult
            participant.UserID = User.Identity.Name
            If ModelState.IsValid Then
                db.Entry(participant).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(participant)
        End Function

        ' GET: Participants/Delete/5
        <Authorize>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim participant As Participant = db.Participants.Find(id)
            If IsNothing(participant) Then
                Return HttpNotFound()
            End If
            Return View(participant)
        End Function

        ' POST: Participants/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim participant As Participant = db.Participants.Find(id)
            db.Participants.Remove(participant)
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
