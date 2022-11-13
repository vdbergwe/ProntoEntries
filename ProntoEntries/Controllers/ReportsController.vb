Imports System.Web.Mvc
Imports System.IO

Namespace Controllers
    Public Class ReportsController
        Inherits Controller

        Private db As New EntriesDBEntities

        ' GET: Reports
        <Authorize>
        Function Index(ByVal RaceId As Integer?, ByVal SearchValue As String) As ActionResult
            ViewBag.SearchText = SearchValue
            Dim OrgId = db.Organisers.Where(Function(a) a.AdminUserID = User.Identity.Name).Select(Function(b) b.OrgID).FirstOrDefault()
            ViewBag.RaceID = New SelectList(db.RaceEvents.Where(Function(a) a.OrgID = OrgId), "RaceID", "RaceName", RaceId)
            ViewBag.SelectedRace = RaceId

            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = RaceId)
            Dim results = db.Participants.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID <> a.ParticipantID))
            If SearchValue IsNot Nothing Then
                Dim PayRef = RaceParticipants.Where(Function(a) a.PayFastReference.Contains(SearchValue))
                If PayRef.Count > 0 Then
                    results = db.Participants.Where(Function(a) PayRef.Any(Function(b) b.ParticipantID <> a.ParticipantID))
                Else
                    results = results.Where(Function(a) a.FirstName.Contains(SearchValue) Or a.LastName.Contains(SearchValue) _
                                                           Or a.IDNumber.Contains(SearchValue) Or a.EmailAddress.Contains(SearchValue))
                End If
            End If


            Return View(results.ToList())
        End Function

        <Authorize>
        Function ExporttoExcel(Id As Integer?)
            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = Id)
            Dim results = db.ParticipantExports.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID <> a.ParticipantID))
            Dim MyData = results.ToList()
            Dim Grid = New GridView With {
                .DataSource = MyData
            }
            Grid.DataBind()

            Response.ClearContent()
            Response.Buffer = True
            Response.AddHeader("content-disposition", "attachment; filename=ProntoEntries_AllParticipants.xls")
            Response.ContentType = "application/xlsx"

            Response.Charset = ""
            Dim sw = New StringWriter()
            Dim htw = New HtmlTextWriter(sw)

            Grid.RenderControl(htw)

            Response.Output.Write(sw.ToString())
            Response.Flush()
            Response.End()

            Return ("")
        End Function

    End Class
End Namespace