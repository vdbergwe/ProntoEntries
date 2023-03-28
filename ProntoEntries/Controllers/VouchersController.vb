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
    Public Class VouchersController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' GET: Vouchers
        Function Index() As ActionResult
            Return View(db.Vouchers.ToList())
        End Function

        ' GET: Vouchers/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim voucher As Voucher = db.Vouchers.Find(id)
            If IsNothing(voucher) Then
                Return HttpNotFound()
            End If
            Return View(voucher)
        End Function

        ' GET: Vouchers/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Vouchers/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="VoucherID,Code,Value,IssuedBy,Pf_Reference,M_Reference,Date,Status,UsedBy,UsedDate,UsedM_Reference")> ByVal voucher As Voucher) As ActionResult
            If ModelState.IsValid Then
                db.Vouchers.Add(voucher)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(voucher)
        End Function

        ' GET: Vouchers/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim voucher As Voucher = db.Vouchers.Find(id)
            If IsNothing(voucher) Then
                Return HttpNotFound()
            End If
            Return View(voucher)
        End Function

        ' POST: Vouchers/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="VoucherID,Code,Value,IssuedBy,Pf_Reference,M_Reference,Date,Status,UsedBy,UsedDate,UsedM_Reference")> ByVal voucher As Voucher) As ActionResult
            If ModelState.IsValid Then
                db.Entry(voucher).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(voucher)
        End Function

        ' GET: Vouchers/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim voucher As Voucher = db.Vouchers.Find(id)
            If IsNothing(voucher) Then
                Return HttpNotFound()
            End If
            Return View(voucher)
        End Function

        ' POST: Vouchers/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim voucher As Voucher = db.Vouchers.Find(id)
            db.Vouchers.Remove(voucher)
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
