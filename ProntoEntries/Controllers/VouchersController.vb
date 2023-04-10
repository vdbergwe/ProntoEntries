Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports ProntoEntries
Imports System.Security.Cryptography
Imports SelectPdf
Imports System.IO

Namespace Controllers
    Public Class VouchersController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        Function Get_ItemName(Id As Integer?) As ActionResult
            ViewBag.ItemName = db.AddonItems.Where(Function(a) a.ItemID = Id).Select(Function(b) b.Description).FirstOrDefault()
            Return PartialView()
        End Function

        <Authorize>
        Function GenerateVoucher(Id As Integer?) As FileResult

            Dim url As String = "https://localhost:44386/Vouchers/IssueVoucher/" + Id.ToString()
            Dim converter As New HtmlToPdf()
            Dim doc As PdfDocument = converter.ConvertUrl(url)
            Dim stream As New MemoryStream()
            doc.Save(stream)
            doc.Close()
            Return File(stream.ToArray(), "application/pdf", "ProntoEntries_Voucher_" + Id.ToString() + ".pdf")

        End Function

        <Authorize>
        Function GenerateVoucherSub(Id As Integer?) As FileResult

            Dim VoucherID = db.Vouchers.Where(Function(a) a.Code = Id).Select(Function(b) b.VoucherID).FirstOrDefault()

            Dim url As String = "https://localhost:44386/Vouchers/IssueVoucher/" + VoucherID.ToString()
            Dim converter As New HtmlToPdf()
            Dim doc As PdfDocument = converter.ConvertUrl(url)
            Dim stream As New MemoryStream()
            doc.Save(stream)
            doc.Close()
            Return File(stream.ToArray(), "application/pdf", "ProntoEntries_Voucher_" + VoucherID.ToString() + ".pdf")

        End Function


        Function IssueVoucher(Id As Integer?) As ActionResult
            Dim Voucher = db.Vouchers.Where(Function(b) b.VoucherID = Id).FirstOrDefault()
            Dim RaceID = Voucher.RaceID
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(RaceID)
            ViewBag.Background = raceEvent.Image


            Return PartialView(Voucher)
        End Function

        Private Shared rng As New RNGCryptoServiceProvider()

        Function GenerateVoucherCode(length As Integer) As String
            Const chars As String = "0123456789"
            Dim data(length - 1) As Byte
            rng.GetBytes(data)
            Dim result(length - 1) As Char
            For i As Integer = 0 To length - 1
                result(i) = chars(data(i) Mod chars.Length)
            Next
            Return New String(result)
        End Function


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
        <Authorize>
        Function Create(Id As Integer?, RaceID As Integer?) As ActionResult
            Dim OrgId = db.CustomUserRoles.Where(Function(a) a.UserEmail = User.Identity.Name And a.Role = "Vouchers").Select(Function(b) b.OrgId).FirstOrDefault()
            ViewBag.RaceID = New SelectList(db.RaceEvents.Where(Function(a) a.OrgID = OrgId), "RaceID", "RaceName", RaceID)
            Dim AddItems = db.AddonItems.Where(Function(a) a.RaceID = RaceID)
            Dim ItemOption = db.AddonOptions.Where(Function(a) AddItems.Any(Function(b) b.ItemID <> a.ItemID) And a.Amount > 0).Distinct()
            ViewBag.ItemList = db.AddonItems.Where(Function(a) ItemOption.Any(Function(b) b.ItemID = a.ItemID)).ToList()

            ViewBag.ItemList = db.AddonOptions.Where(Function(a) a.Amount > 0 And AddItems.Any(Function(b) b.ItemID <> a.ItemID)).GroupBy(Function(a) a.ItemID).Select(Function(g) g.FirstOrDefault())
            ViewBag.RaceOptions = db.Divisions.Where(Function(a) a.RaceID = RaceID).GroupBy(Function(a) a.Distance).Select(Function(g) g.FirstOrDefault())

            ViewBag.RaceSeleted = RaceID
            Return View()
        End Function

        ' POST: Vouchers/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="VoucherID,Code,Value,IssuedBy,Pf_Reference,M_Reference,Date,Status,UsedBy,UsedDate,UsedM_Reference,RaceID")> ByVal voucher As Voucher, RaceID As Integer?) As ActionResult
            voucher.Code = "22" + GenerateVoucherCode(7)
            Dim flag As Boolean = False

            While flag = False
                If db.Vouchers.Where(Function(a) a.Code = voucher.Code).Count() = 0 Then
                    flag = True
                Else
                    voucher.Code = "22" + GenerateVoucherCode(7)
                End If
            End While

            voucher.IssuedBy = User.Identity.Name
            voucher.Date = Now()
            voucher.Status = "Active"

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
        Function Edit(<Bind(Include:="VoucherID,Code,Value,IssuedBy,Pf_Reference,M_Reference,Date,Status,UsedBy,UsedDate,UsedM_Reference,RaceID")> ByVal voucher As Voucher) As ActionResult
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
            voucher.Status = "Revoked"
            voucher.UsedBy = User.Identity.Name
            voucher.UsedDate = Now()
            If ModelState.IsValid Then
                db.Entry(voucher).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(voucher)
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
