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
            Dim results = db.Participants.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID = a.ParticipantID))
            If SearchValue IsNot Nothing Then
                If SearchValue.Length() > 0 Then
                    Dim PayRef = RaceParticipants.Where(Function(a) a.PayFastReference.Contains(SearchValue))
                    If PayRef.Count > 0 Then
                        results = db.Participants.Where(Function(a) PayRef.Any(Function(b) b.ParticipantID = a.ParticipantID))
                    Else
                        results = results.Where(Function(a) a.FirstName.Contains(SearchValue) Or a.LastName.Contains(SearchValue) _
                                                               Or a.IDNumber.Contains(SearchValue) Or a.EmailAddress.Contains(SearchValue))
                    End If
                End If
            End If


            Return View(results.ToList())
        End Function

        <Authorize>
        Function Dashboard(ByVal RaceId As Integer?, ByVal SearchValue As String) As ActionResult
            ViewBag.SearchText = SearchValue
            Dim OrgId = db.Organisers.Where(Function(a) a.AdminUserID = User.Identity.Name).Select(Function(b) b.OrgID).FirstOrDefault()
            ViewBag.RaceID = New SelectList(db.RaceEvents.Where(Function(a) a.OrgID = OrgId), "RaceID", "RaceName", RaceId)
            ViewBag.SelectedRace = RaceId

            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = RaceId)
            Dim results = db.Participants.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID = a.ParticipantID))
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

        Function Get_EntriesPerDivision(ByVal RaceId As Integer?) As ActionResult
            Dim EventDivisions = db.Divisions.Where(Function(a) a.RaceID = RaceId)

            Return PartialView(EventDivisions)
        End Function

        Function Get_OptionTotals(ByVal RaceId As Integer?) As ActionResult
            Dim EventAddons = db.AddonItems.Where(Function(a) a.RaceID = RaceId)
            Dim EventOptions = db.AddonOptions.Where(Function(a) EventAddons.Any(Function(b) b.ItemID <> a.ItemID)).OrderBy(Function(c) c.ItemID)

            Return PartialView(EventOptions)
        End Function

        Function Get_Financials(ByVal RaceId As Integer?) As ActionResult
            Dim EventDivisions = db.Divisions.Where(Function(a) a.RaceID = RaceId)

            Return PartialView(EventDivisions)
        End Function

        Function Get_FinancialsAddons(ByVal RaceId As Integer?) As ActionResult
            Dim EventAddons = db.AddonItems.Where(Function(a) a.RaceID = RaceId)
            Dim EventOptions = db.AddonOptions.Where(Function(a) EventAddons.Any(Function(b) b.ItemID <> a.ItemID)).OrderBy(Function(c) c.ItemID)

            Return PartialView(EventOptions)
        End Function

        Function Get_PayFast(ByVal RaceId As Integer?) As ActionResult
            Dim AllPFReferences = db.Sales.Where(Function(a) a.RaceID = RaceId).Distinct()
            Dim PFTransactions = db.pflogs.Where(Function(a) AllPFReferences.Any(Function(b) b.Pf_reference <> a.Pf_reference))
            ViewBag.Amount_Gross = PFTransactions.Select(Function(a) a.amount_gross).Sum()
            ViewBag.Amount_Fee = PFTransactions.Select(Function(a) a.amount_fee).Sum()
            ViewBag.Amount_Net = PFTransactions.Select(Function(a) a.amount_net).Sum()


            Return PartialView()
        End Function




        Function Get_DivisionTotal(Id As Integer?) As ActionResult
            ViewBag.DivisionTotal = db.Entries.Where(Function(a) a.DivisionID = Id).Count()
            Return PartialView()
        End Function

        Function Get_OptionCount(Id As Integer?) As ActionResult
            ViewBag.OptionCount = db.Sales.Where(Function(a) a.OptionID = Id).Count()
            Return PartialView()
        End Function

        Function Get_DivisionTotalAmount(Id As Integer?) As ActionResult
            Dim DivisionEntryCount = db.Entries.Where(Function(a) a.DivisionID = Id).Count()
            Dim DivisionPrice = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Price).FirstOrDefault()
            ViewBag.DivisionTotalAmount = DivisionEntryCount * DivisionPrice

            Return PartialView()
        End Function

        Function Get_AddonTotalAmount(Id As Integer?) As ActionResult
            Dim OptionCount = db.Sales.Where(Function(a) a.OptionID = Id).Count()
            Dim OptionPrice = db.AddonOptions.Where(Function(a) a.OptionID = Id).Select(Function(b) b.Amount).FirstOrDefault()
            ViewBag.AddonTotalAmount = OptionCount * OptionPrice

            Return PartialView()
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

        <Authorize>
        Function ExporttoExcelRaceDetail(Id As Integer?)
            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = Id)
            Dim results = db.PartDivs.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID = a.ParticipantID))
            Dim MyData = results.ToList()

            'Using ExcelEngine As ExcelEngine = New ExcelEngine()
            '    Dim application As IApplication = ExcelEngine.Excel

            '    application.DefaultVersion = ExcelVersion.Xlsx

            '    Dim Workbook As IWorkbook = application.Workbooks.Create(1)
            '    Dim worksheet As IWorksheet = Workbook.Worksheets(0)

            '    worksheet.Range(1, 1).Value = "First Name"
            '    worksheet.Range(1, 2).Value = "Middle Name"
            '    worksheet.Range(1, 3).Value = "Last Name"
            '    worksheet.Range(1, 4).Value = "ID Number"
            '    worksheet.Range(1, 5).Value = "Date of Birth"
            '    worksheet.Range(1, 6).Value = "Gender"
            '    worksheet.Range(1, 7).Value = "License Number"
            '    worksheet.Range(1, 8).Value = "Email Address"
            '    worksheet.Range(1, 9).Value = "Medical Name"
            '    worksheet.Range(1, 10).Value = "Medical Number"
            '    worksheet.Range(1, 11).Value = "Emergency Contact"
            '    worksheet.Range(1, 12).Value = "Emergency Number"
            '    worksheet.Range(1, 13).Value = "Blood Type"
            '    worksheet.Range(1, 14).Value = "Allergies"
            '    worksheet.Range(1, 15).Value = "Additional Info"
            '    worksheet.Range(1, 16).Value = "Doctor Name"
            '    worksheet.Range(1, 17).Value = "Doctor Contact"
            '    worksheet.Range(1, 18).Value = "Club Name"
            '    worksheet.Range(1, 19).Value = "Country"
            '    worksheet.Range(1, 20).Value = "Address"
            '    worksheet.Range(1, 21).Value = "City"
            '    worksheet.Range(1, 22).Value = "Province"
            '    worksheet.Range(1, 23).Value = "Distance"
            '    worksheet.Range(1, 24).Value = "Category"
            '    worksheet.Range(1, 25).Value = "Description"
            '    worksheet.Range(1, 26).Value = "Start Time"
            '    worksheet.Range(1, 27).Value = "Price"
            '    worksheet.Range(1, 28).Value = "M_reference"
            '    worksheet.Range(1, 29).Value = "Pf_Reference"
            '    worksheet.Range(1, 30).Value = "RaceID"


            '    For Each record In MyData

            '        worksheet.Range(2, 2).Value = "Test"


            '        'For i = 1 To MyData.GetType().GetProperties().Length
            '        '    For j = 1 To MyData.Count()
            '        '        worksheet.Range(i, j).Value = "Test"
            '        '    Next
            '        'Next
            '    Next

            '    Workbook.SaveAs("Output.xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open)

            'End Using


            Return ("")
        End Function

        '<Authorize>
        'Function ExporttoExcelRaceDetail(Id As Integer?)
        '    Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = Id)
        '    Dim results = db.PartDivs.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID = a.ParticipantID))
        '    Dim MyData = results.ToList()


        '    Dim Grid = New GridView With {
        '        .DataSource = MyData
        '    }
        '    Grid.DataBind()



        '    Response.ClearContent()
        '    Response.Buffer = True
        '    Response.AddHeader("content-disposition", "attachment; filename=ProntoEntries_FullRaceDetail.xls")
        '    Response.ContentType = "application/x-ms-excel"

        '    Response.Charset = ""
        '    Dim sw = New StringWriter()
        '    Dim htw = New HtmlTextWriter(sw)

        '    Grid.RenderControl(htw)

        '    Response.Output.Write(sw)
        '    Response.Flush()
        '    Response.End()

        '    Return ("")
        'End Function

    End Class
End Namespace