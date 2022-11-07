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
    Public Class AddonSalesController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' GET: AddonSales
        Function Index() As ActionResult
            Return View(db.AddonSales.ToList())
        End Function

        ' GET: AddonSales/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonSale As AddonSale = db.AddonSales.Find(id)
            If IsNothing(addonSale) Then
                Return HttpNotFound()
            End If
            Return View(addonSale)
        End Function

        ' GET: AddonSales/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: AddonSales/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="ItemID,Name,Description,Size,Amount,EntryID,RaceID")> ByVal addonSale As AddonSale) As ActionResult
            If ModelState.IsValid Then
                db.AddonSales.Add(addonSale)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(addonSale)
        End Function

        ' GET: AddonSales/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonSale As AddonSale = db.AddonSales.Find(id)
            If IsNothing(addonSale) Then
                Return HttpNotFound()
            End If
            Return View(addonSale)
        End Function

        ' POST: AddonSales/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="ItemID,Name,Description,Size,Amount,EntryID,RaceID")> ByVal addonSale As AddonSale) As ActionResult
            If ModelState.IsValid Then
                db.Entry(addonSale).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(addonSale)
        End Function

        ' GET: AddonSales/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonSale As AddonSale = db.AddonSales.Find(id)
            If IsNothing(addonSale) Then
                Return HttpNotFound()
            End If
            Return View(addonSale)
        End Function

        ' POST: AddonSales/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim addonSale As AddonSale = db.AddonSales.Find(id)
            db.AddonSales.Remove(addonSale)
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
