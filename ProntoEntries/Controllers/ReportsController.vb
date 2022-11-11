Imports System.Web.Mvc
Imports System.IO

Namespace Controllers
    Public Class ReportsController
        Inherits Controller

        Private db As New EntriesDBEntities

        ' GET: Reports
        Function Index(ByVal RaceId As Integer?, ByVal SearchText As String) As ActionResult
            ViewBag.SearchText = SearchText
            Dim OrgId = db.Organisers.Where(Function(a) a.AdminUserID = User.Identity.Name).Select(Function(b) b.OrgID).FirstOrDefault()
            ViewBag.RaceID = New SelectList(db.RaceEvents.Where(Function(a) a.OrgID = OrgId), "RaceID", "RaceName", RaceId)
            ViewBag.SelectedRace = RaceId

            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = RaceId)
            Dim results = db.Participants.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID <> a.ParticipantID))
            If SearchText IsNot Nothing Then
                Dim PayRef = RaceParticipants.Where(Function(a) a.PayFastReference.Contains(SearchText))
                If PayRef.Count > 0 Then
                    results = db.Participants.Where(Function(a) PayRef.Any(Function(b) b.ParticipantID <> a.ParticipantID))
                Else
                    results = results.Where(Function(a) a.FirstName.Contains(SearchText) Or a.LastName.Contains(SearchText) _
                                                           Or a.IDNumber.Contains(SearchText) Or a.EmailAddress.Contains(SearchText))
                End If
            End If


            Return View(results.ToList())
        End Function

        Function ExporttoExcel(Id As Integer?)
            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = Id)
            Dim results = db.Participants.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID <> a.ParticipantID))
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