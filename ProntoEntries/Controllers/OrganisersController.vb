Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries
Imports System.IO

Namespace Controllers
    Public Class OrganisersController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' GET: Organisers
        <Authorize>
        Function Index() As ActionResult
            Return View(db.Organisers.ToList())
        End Function

        ' GET: Organisers/Details/5
        <Authorize>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim organiser As Organiser = db.Organisers.Find(id)
            If IsNothing(organiser) Then
                Return HttpNotFound()
            End If
            Return View(organiser)
        End Function

        ' GET: Organisers/Create
        <Authorize>
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Organisers/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Create(<Bind(Include:="OrgID,OrgName,OrgEmail,OrgTel,OrgWebsite,OrgVatNumber,AdminUserID,Image")> ByVal organiser As Organiser, imgFile As HttpPostedFileBase) As ActionResult
            Dim ImgPath As String
            If ModelState.IsValid Then
                If (imgFile.FileName.Length > 0) Then
                    ImgPath = "~/Content/" + Path.GetFileName(imgFile.FileName)
                    imgFile.SaveAs(Server.MapPath(ImgPath))
                    organiser.Image = ImgPath.ToString()
                End If
                db.Organisers.Add(organiser)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If

            Return View(organiser)
        End Function

        ' GET: Organisers/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim organiser As Organiser = db.Organisers.Find(id)
            If IsNothing(organiser) Then
                Return HttpNotFound()
            End If
            Return View(organiser)
        End Function

        ' POST: Organisers/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Edit(<Bind(Include:="OrgID,OrgName,OrgEmail,OrgTel,OrgWebsite,OrgVatNumber,AdminUserID,Image")> ByVal organiser As Organiser) As ActionResult
            If ModelState.IsValid Then
                db.Entry(organiser).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(organiser)
        End Function

        ' GET: Organisers/Delete/5
        <Authorize>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim organiser As Organiser = db.Organisers.Find(id)
            If IsNothing(organiser) Then
                Return HttpNotFound()
            End If
            Return View(organiser)
        End Function

        ' POST: Organisers/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim organiser As Organiser = db.Organisers.Find(id)
            db.Organisers.Remove(organiser)
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
