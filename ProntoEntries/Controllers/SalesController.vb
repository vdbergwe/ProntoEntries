﻿Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries

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
            Return Result
        End Function

        ' GET: Sales
        Function Index() As ActionResult
            Return View(db.Sales.ToList())
        End Function

        ' GET: Sales/Details/5
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
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Sales/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="SaleID,RaceID,DivisionID,ItemID,UserID,Indemnity,TandC,ParticipantID,M_reference,Pf_reference,OptionID,Verified")> ByVal sale As Sale) As ActionResult
            If ModelState.IsValid Then
                db.Sales.Add(sale)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(sale)
        End Function

        ' GET: Sales/Edit/5
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
        Function Edit(<Bind(Include:="SaleID,RaceID,DivisionID,ItemID,UserID,Indemnity,TandC,ParticipantID,M_reference,Pf_reference,OptionID,Verified")> ByVal sale As Sale) As ActionResult
            If ModelState.IsValid Then
                db.Entry(sale).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(sale)
        End Function

        ' GET: Sales/Delete/5
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
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim sale As Sale = db.Sales.Find(id)
            db.Sales.Remove(sale)
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
