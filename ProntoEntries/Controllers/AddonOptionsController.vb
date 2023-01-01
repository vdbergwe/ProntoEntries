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
    Public Class AddonOptionsController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        Function Get_ItemName(Id As Integer?) As ActionResult
            ViewBag.ItemName = db.AddonItems.Where(Function(a) a.ItemID = Id).Select(Function(b) b.Name).FirstOrDefault()
            Return PartialView()
        End Function

        Function Get_ItemSize(Id As Integer?) As ActionResult
            ViewBag.ItemSize = db.AddonOptions.Where(Function(a) a.OptionID = Id).Select(Function(b) b.Size).FirstOrDefault()
            Return PartialView()
        End Function

        ' GET: AddonOptions
        <Authorize>
        Function IndexPartial(Id As Integer?) As ActionResult
            Dim Options = db.AddonOptions.Where(Function(a) a.ItemID = Id).ToList()
            Return PartialView(Options)
        End Function

        <Authorize>
        Function ViewAddOns(Id As Integer?, ParticipantID As Integer?) As ActionResult
            Dim Purchased = db.Sales.Where(Function(b) b.M_reference = Id And b.ParticipantID = ParticipantID And b.RaceID Is Nothing)
            Dim Options = db.AddonOptions.Where(Function(b) Purchased.Any(Function(a) a.OptionID = b.OptionID)).OrderBy(Function(c) c.ItemID)
            Return PartialView(Options)
        End Function

        Function ViewAddOnsTicket(Id As Integer?, ParticipantID As Integer?) As ActionResult
            Dim Purchased = db.Sales.Where(Function(b) b.M_reference = Id And b.ParticipantID = ParticipantID And b.RaceID Is Nothing)
            Dim Options = db.AddonOptions.Where(Function(b) Purchased.Any(Function(a) a.OptionID = b.OptionID)).OrderBy(Function(c) c.ItemID)
            Return PartialView(Options)
        End Function

        <Authorize>
        Function Index() As ActionResult
            Return View(db.AddonOptions.ToList())
        End Function

        ' GET: AddonOptions/Details/5
        <Authorize>
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonOption As AddonOption = db.AddonOptions.Find(id)
            If IsNothing(addonOption) Then
                Return HttpNotFound()
            End If
            Return View(addonOption)
        End Function

        ' GET: AddonOptions/Create
        <Authorize>
        Function Create(Id As Integer?) As ActionResult
            ViewBag.ItemID = Id
            Return View()
        End Function

        ' POST: AddonOptions/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Create(<Bind(Include:="OptionID,ItemID,Size,Amount,StopDate")> ByVal addonOption As AddonOption, Id As Integer?) As ActionResult
            addonOption.ItemID = Id
            If ModelState.IsValid Then
                db.AddonOptions.Add(addonOption)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(addonOption)
        End Function

        ' GET: AddonOptions/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonOption As AddonOption = db.AddonOptions.Find(id)
            If IsNothing(addonOption) Then
                Return HttpNotFound()
            End If
            Return View(addonOption)
        End Function

        ' POST: AddonOptions/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Edit(<Bind(Include:="OptionID,ItemID,Size,Amount,StopDate")> ByVal addonOption As AddonOption) As ActionResult
            If ModelState.IsValid Then
                db.Entry(addonOption).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(addonOption)
        End Function

        ' GET: AddonOptions/Delete/5
        <Authorize>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim addonOption As AddonOption = db.AddonOptions.Find(id)
            If IsNothing(addonOption) Then
                Return HttpNotFound()
            End If
            Return View(addonOption)
        End Function

        ' POST: AddonOptions/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim addonOption As AddonOption = db.AddonOptions.Find(id)
            db.AddonOptions.Remove(addonOption)
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
