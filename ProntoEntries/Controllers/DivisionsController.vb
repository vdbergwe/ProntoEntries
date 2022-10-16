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
    Public Class DivisionsController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' GET: Divisions
        Function Index() As ActionResult
            Return View(db.Divisions.ToList())
        End Function

        ' GET: Divisions/Details/5
        Function ViewClasses(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim division = db.Divisions.Where(Function(a) a.RaceID = id)
            If IsNothing(division) Then
                Return HttpNotFound()
            End If
            Return PartialView(division)
        End Function

        ' GET: Divisions/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim division As Division = db.Divisions.Find(id)
            If IsNothing(division) Then
                Return HttpNotFound()
            End If
            Return View(division)
        End Function

        ' GET: Divisions/Create
        Function Create() As ActionResult
            ViewBag.RaceID = New SelectList(db.RaceEvents, "RaceID", "RaceName")
            Return View()
        End Function

        ' POST: Divisions/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="DivisionID,Distance,Category,Description,StartTime,Price,RaceID")> ByVal division As Division) As ActionResult
            ViewBag.RaceID = New SelectList(db.RaceEvents, "RaceID", "RaceName")
            If ModelState.IsValid Then
                db.Divisions.Add(division)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(division)
        End Function

        ' GET: Divisions/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim division As Division = db.Divisions.Find(id)
            If IsNothing(division) Then
                Return HttpNotFound()
            End If
            Return View(division)
        End Function

        ' POST: Divisions/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="DivisionID,Distance,Category,Description,StartTime,Price,RaceID")> ByVal division As Division) As ActionResult
            If ModelState.IsValid Then
                db.Entry(division).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(division)
        End Function

        ' GET: Divisions/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim division As Division = db.Divisions.Find(id)
            If IsNothing(division) Then
                Return HttpNotFound()
            End If
            Return View(division)
        End Function

        ' POST: Divisions/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim division As Division = db.Divisions.Find(id)
            db.Divisions.Remove(division)
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
