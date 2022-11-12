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
    Public Class RaceEventsController
        Inherits System.Web.Mvc.Controller

        Private db As New EntriesDBEntities

        ' GET: RaceEvents
        Function Index() As ActionResult

            Return View(db.RaceEvents.ToList())
        End Function

        Function get_devdistance(Id As Integer?) As ActionResult
            Dim mindist = String.Format("{0:n1}", db.Divisions.Where(Function(x) x.RaceID = Id).Min(Function(x) x.Distance))
            Dim maxdist = String.Format("{0:n1}", db.Divisions.Where(Function(x) x.RaceID = Id).Max(Function(x) x.Distance))
            ViewBag.getdevdistance = mindist.ToString + " - " + maxdist.ToString + "km"
            Return PartialView()
        End Function

        Function get_devprice(Id As Integer?) As ActionResult
            Dim Lowprice = String.Format("{0:n0}", db.Divisions.Where(Function(x) x.RaceID = Id).Min(Function(x) x.Price))
            Dim Highprice = String.Format("{0:n0}", db.Divisions.Where(Function(x) x.RaceID = Id).Max(Function(x) x.Price))
            ViewBag.getdevprice = "R" + Lowprice.ToString + " - " + "R" + Highprice.ToString
            Return PartialView()
        End Function

        ' GET: RaceEvents/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            Dim Organiser As Organiser = db.Organisers.Find(raceEvent.OrgID)
            ViewBag.RaceName = raceEvent.RaceName
            ViewBag.Background = raceEvent.Background
            ViewBag.OrgID = Organiser.OrgName
            ViewBag.OrgImage = Organiser.Image
            ViewBag.RaceDate = raceEvent.RaceDate.Value.ToString("dddd, dd MMMM yyyy")
            If raceEvent.EntriesCloseDate < Now() Then
                ViewBag.Closed = True
            End If
            If IsNothing(raceEvent) Then
                Return HttpNotFound()
            End If
            Return View(raceEvent)
        End Function

        ' GET: RaceEvents/Create
        <Authorize>
        Function Create() As ActionResult
            ViewBag.OrgID = New SelectList(db.Organisers, "OrgId", "OrgName")
            Return View()
        End Function

        ' POST: RaceEvents/Create
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function Create(<Bind(Include:="RaceID,RaceName,RaceDescription,RaceDate,RaceType,Coordinates,Address,City,Province,AdminCharge,OrgID,Image,Background,ImgDetail1,ImgDetail2,ImgDetail3,DispClasses,DispAdmCharge,DispAddress,RaceHtmlPage")> ByVal raceEvent As RaceEvent, imgFile2 As HttpPostedFileBase, imgBackground As HttpPostedFileBase) As ActionResult
            Dim ImgPath1 As String
            Dim ImgPath2 As String
            If ModelState.IsValid Then
                If IsNothing(imgFile2) = False Then
                    ImgPath1 = "~/Content/RaceLogo/" + Path.GetFileName(imgFile2.FileName)
                    imgFile2.SaveAs(Server.MapPath(ImgPath1))
                    raceEvent.Image = ImgPath1.ToString()
                End If
                If IsNothing(imgBackground) = False Then
                    ImgPath2 = "~/Content/OrgBackground/" + Path.GetFileName(imgBackground.FileName)
                    imgBackground.SaveAs(Server.MapPath(ImgPath2))
                    raceEvent.Background = ImgPath2.ToString()
                End If
                raceEvent.DispClasses = True
                raceEvent.DispAdmCharge = True
                raceEvent.DispAddress = True
                db.RaceEvents.Add(raceEvent)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(raceEvent)
        End Function

        ' GET: RaceEvents/Edit/5
        <Authorize>
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            ViewBag.OrgID = New SelectList(db.Organisers, "OrgId", "OrgName", raceEvent.OrgID)
            ViewBag.Background = raceEvent.Background
            ViewBag.Image = raceEvent.Image
            If IsNothing(raceEvent) Then
                Return HttpNotFound()
            End If
            Return View(raceEvent)
        End Function

        ' POST: RaceEvents/Edit/5
        'To protect from overposting attacks, enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <Authorize>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="RaceID,RaceName,RaceDescription,RaceDate,RaceType,Coordinates,Address,City,Province,AdminCharge,OrgID,Image,Background,ImgDetail1,ImgDetail2,ImgDetail3,DispClasses,DispAdmCharge,DispAddress,RaceHtmlPage")> ByVal raceEvent As RaceEvent, imgFile2 As HttpPostedFileBase, imgBackground As HttpPostedFileBase, imgdetailfirst As HttpPostedFileBase, imgdetailsecond As HttpPostedFileBase, imgdetailthird As HttpPostedFileBase) As ActionResult
            Dim ImgPath As String

            If ModelState.IsValid Then
                If IsNothing(imgFile2) = False Then
                    ImgPath = "~/Content/RaceLogo/" + Path.GetFileName(imgFile2.FileName)
                    imgFile2.SaveAs(Server.MapPath(ImgPath))
                    raceEvent.Image = ImgPath.ToString()
                End If
                If IsNothing(imgBackground) = False Then
                    ImgPath = "~/Content/OrgBackground/" + Path.GetFileName(imgBackground.FileName)
                    imgBackground.SaveAs(Server.MapPath(ImgPath))
                    raceEvent.Background = ImgPath.ToString()
                End If
                If IsNothing(imgdetailfirst) = False Then
                    ImgPath = "~/Content/RaceDetailImages/" + Path.GetFileName(imgdetailfirst.FileName)
                    imgdetailfirst.SaveAs(Server.MapPath(ImgPath))
                    raceEvent.Background = ImgPath.ToString()
                End If
                If IsNothing(imgdetailsecond) = False Then
                    ImgPath = "~/Content/RaceDetailImages/" + Path.GetFileName(imgdetailsecond.FileName)
                    imgdetailsecond.SaveAs(Server.MapPath(ImgPath))
                    raceEvent.Background = ImgPath.ToString()
                End If
                If IsNothing(imgdetailthird) = False Then
                    ImgPath = "~/Content/RaceDetailImages/" + Path.GetFileName(imgdetailthird.FileName)
                    imgdetailthird.SaveAs(Server.MapPath(ImgPath))
                    raceEvent.Background = ImgPath.ToString()
                End If

                db.Entry(raceEvent).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(raceEvent)
        End Function

        ' GET: RaceEvents/Delete/5
        <Authorize>
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            If IsNothing(raceEvent) Then
                Return HttpNotFound()
            End If
            Return View(raceEvent)
        End Function

        ' POST: RaceEvents/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        <Authorize>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim raceEvent As RaceEvent = db.RaceEvents.Find(id)
            db.RaceEvents.Remove(raceEvent)
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
