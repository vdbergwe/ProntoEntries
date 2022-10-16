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

        ' GET: Participants
        Function Index() As ActionResult
            Return View(db.Participants.ToList())
        End Function

        ' GET: Participants/Details/5
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
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Participants/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ParticipantID,FirstName,MiddleNames,LastName,IDNumber,Day,Month,Year,RaceNumber,EmailAddress,MedicalName,MedicalNumber,EmergencyContact,EmergencyNumber,BoodType,Allergies,AdditionalInfo,DoctorName,DoctorContact,Clubname,Country,Address,City,Province,UserID,EventMailer,Offers")> ByVal participant As Participant) As ActionResult
            If ModelState.IsValid Then
                db.Participants.Add(participant)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(participant)
        End Function

        ' GET: Participants/Edit/5
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
        Function Edit(<Bind(Include:="ParticipantID,FirstName,MiddleNames,LastName,IDNumber,Day,Month,Year,RaceNumber,EmailAddress,MedicalName,MedicalNumber,EmergencyContact,EmergencyNumber,BoodType,Allergies,AdditionalInfo,DoctorName,DoctorContact,Clubname,Country,Address,City,Province,UserID,EventMailer,Offers")> ByVal participant As Participant) As ActionResult
            If ModelState.IsValid Then
                db.Entry(participant).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(participant)
        End Function

        ' GET: Participants/Delete/5
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
