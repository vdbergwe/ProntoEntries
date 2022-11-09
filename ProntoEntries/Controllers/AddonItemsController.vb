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
    Public Class AddonItemsController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        Function get_RaceName(Id As Integer?) As ActionResult
            ViewBag.RaceName = db.RaceEvents.Where(Function(a) a.RaceID = Id).Select(Function(b) b.RaceName).FirstOrDefault()
            Return PartialView()
        End Function

        ' GET: AddonItems
        Function Index() As ActionResult
            ViewBag.RaceID = New SelectList(db.RaceEvents, "RaceID", "RaceName")
            Return View(db.AddonItems.ToList())
        End Function

        ' GET: AddonItems/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonItem As AddonItem = db.AddonItems.Find(id)
            If IsNothing(addonItem) Then
                Return HttpNotFound()
            End If
            Return View(addonItem)
        End Function

        ' GET: AddonItems/Create
        Function Create() As ActionResult
            ViewBag.RaceID = New SelectList(db.RaceEvents, "RaceID", "RaceName")
            Return View()
        End Function

        ' POST: AddonItems/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ItemID,RaceID,Name,Description")> ByVal addonItem As AddonItem) As ActionResult
            If ModelState.IsValid Then
                db.AddonItems.Add(addonItem)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(addonItem)
        End Function

        ' GET: AddonItems/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonItem As AddonItem = db.AddonItems.Find(id)
            If IsNothing(addonItem) Then
                Return HttpNotFound()
            End If
            Return View(addonItem)
        End Function

        ' POST: AddonItems/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ItemID,RaceID,Name,Description")> ByVal addonItem As AddonItem) As ActionResult
            If ModelState.IsValid Then
                db.Entry(addonItem).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(addonItem)
        End Function

        ' GET: AddonItems/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonItem As AddonItem = db.AddonItems.Find(id)
            If IsNothing(addonItem) Then
                Return HttpNotFound()
            End If
            Return View(addonItem)
        End Function

        ' POST: AddonItems/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim addonItem As AddonItem = db.AddonItems.Find(id)
            db.AddonItems.Remove(addonItem)
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
