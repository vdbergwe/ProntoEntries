Imports System.Web.Mvc
Imports System.IO
Imports ClosedXML.Excel

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

            If User.IsInRole("Admin") Then
                ViewBag.RaceID = New SelectList(db.RaceEvents, "RaceID", "RaceName", RaceId)
            End If

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

            If User.IsInRole("Admin") Then
                ViewBag.RaceID = New SelectList(db.RaceEvents, "RaceID", "RaceName", RaceId)
            End If

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

            ViewBag.TotalEntries = results.Count()

            Return View(results.ToList())
        End Function

        Function Get_TotalEntries(ByVal RaceId As Integer?)
            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = RaceId)

            ViewBag.TotalEntries = RaceParticipants.Count()

            Return PartialView()
        End Function

        Function EntryDetail(ByVal Id As Integer?, ByVal RaceID As Integer?, ByVal SearchValue As String) As ActionResult
            Dim Participant = db.Participants.Where(Function(a) a.ParticipantID = Id).FirstOrDefault()
            ViewBag.RaceID = RaceID
            ViewBag.SearchValue = SearchValue

            Dim Entry = db.Entries.Where(Function(a) a.ParticipantID = Id And RaceID = RaceID).FirstOrDefault()

            ViewBag.Distance = db.Divisions.Where(Function(a) a.DivisionID = Entry.DivisionID).Select(Function(b) b.Distance).FirstOrDefault()
            ViewBag.Category = db.Divisions.Where(Function(a) a.DivisionID = Entry.DivisionID).Select(Function(b) b.Category).FirstOrDefault()
            ViewBag.Collection = db.Divisions.Where(Function(a) a.DivisionID = Entry.DivisionID).Select(Function(b) b.Description).FirstOrDefault()

            'Addon
            ViewBag.ParticipantID = Entry.ParticipantID


            ViewBag.Pfref = Entry.PayFastReference
            ViewBag.Mref = db.pflogs.Where(Function(a) a.Pf_reference = Entry.PayFastReference).Select(Function(b) b.M_reference).FirstOrDefault()
            ViewBag.Amount = db.pflogs.Where(Function(a) a.Pf_reference = Entry.PayFastReference).Select(Function(b) b.amount_gross).FirstOrDefault()



            Return View(Participant)
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
            Dim PFTransactions = db.pflogs.Where(Function(a) AllPFReferences.Any(Function(b) b.Pf_reference = a.Pf_reference))
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
            ViewBag.OptionCount = db.Sales.Where(Function(a) a.OptionID = Id And a.Pf_reference IsNot Nothing).Count()
            Return PartialView()
        End Function

        Function Get_DivisionTotalAmount(Id As Integer?) As ActionResult
            Dim DivisionEntryCount = db.Entries.Where(Function(a) a.DivisionID = Id).Count()
            Dim DivisionPrice = db.Divisions.Where(Function(a) a.DivisionID = Id).Select(Function(b) b.Price).FirstOrDefault()
            ViewBag.DivisionTotalAmount = DivisionEntryCount * DivisionPrice

            Return PartialView()
        End Function

        Function Get_AddonTotalAmount(Id As Integer?) As ActionResult
            Dim OptionCount = db.Sales.Where(Function(a) a.OptionID = Id And a.Pf_reference IsNot Nothing).Count()
            Dim OptionPrice = db.AddonOptions.Where(Function(a) a.OptionID = Id).Select(Function(b) b.Amount).FirstOrDefault()
            ViewBag.AddonTotalAmount = OptionCount * OptionPrice

            Return PartialView()
        End Function

        <Authorize>
        Function ExporttoExcel(Id As Integer?)
            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = Id)
            Dim results = db.ParticipantExports.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID = a.ParticipantID))
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


            Dim workbook As New XLWorkbook()
            Dim stream As New MemoryStream()

            Using workbook
                Dim worksheet = workbook.AddWorksheet("Report")


                worksheet.Cell(1, 1).Value = "First Name"
                worksheet.Cell(1, 2).Value = "Middle Name"
                worksheet.Cell(1, 3).Value = "Last Name"
                worksheet.Cell(1, 4).Value = "ID Number"
                worksheet.Cell(1, 5).Value = "Date of Birth"
                worksheet.Cell(1, 6).Value = "Gender"
                worksheet.Cell(1, 7).Value = "License Number"
                worksheet.Cell(1, 8).Value = "Email Address"
                worksheet.Cell(1, 9).Value = "Mobile"
                worksheet.Cell(1, 10).Value = "Medical Name"
                worksheet.Cell(1, 11).Value = "Medical Number"
                worksheet.Cell(1, 12).Value = "Emergency Contact"
                worksheet.Cell(1, 13).Value = "Emergency Number"
                worksheet.Cell(1, 14).Value = "Blood Type"
                worksheet.Cell(1, 15).Value = "Allergies"
                worksheet.Cell(1, 16).Value = "Additional Info"
                worksheet.Cell(1, 17).Value = "Doctor Name"
                worksheet.Cell(1, 18).Value = "Doctor Contact"
                worksheet.Cell(1, 19).Value = "Club Name"
                worksheet.Cell(1, 20).Value = "Country"
                worksheet.Cell(1, 21).Value = "Address"
                worksheet.Cell(1, 22).Value = "City"
                worksheet.Cell(1, 23).Value = "Province"
                worksheet.Cell(1, 24).Value = "Distance"
                worksheet.Cell(1, 25).Value = "Category"
                worksheet.Cell(1, 26).Value = "Description"
                worksheet.Cell(1, 27).Value = "Start Time"
                worksheet.Cell(1, 28).Value = "Price"
                worksheet.Cell(1, 29).Value = "M_reference"
                worksheet.Cell(1, 30).Value = "Pf_Reference"
                worksheet.Cell(1, 31).Value = "RaceID"

                Dim row = 2
                Dim Newtime As DateTime

                For Each record In MyData
                    worksheet.Cell(row, 1).Value = record.FirstName
                    worksheet.Cell(row, 2).Value = record.MiddleNames
                    worksheet.Cell(row, 3).Value = record.LastName
                    worksheet.Cell(row, 4).SetValue(record.IDNumber)
                    worksheet.Cell(row, 5).Value = record.DOB
                    worksheet.Cell(row, 6).Value = record.Gender
                    worksheet.Cell(row, 7).Value = record.RaceNumber
                    worksheet.Cell(row, 8).Value = record.EmailAddress
                    worksheet.Cell(row, 9).Value = record.Mobile
                    worksheet.Cell(row, 10).Value = record.MedicalName
                    worksheet.Cell(row, 11).SetValue(record.MedicalNumber)
                    worksheet.Cell(row, 12).Value = record.EmergencyContact
                    worksheet.Cell(row, 13).SetValue(record.EmergencyNumber)
                    worksheet.Cell(row, 14).Value = record.BoodType
                    worksheet.Cell(row, 15).Value = record.Allergies
                    worksheet.Cell(row, 16).Value = record.AdditionalInfo
                    worksheet.Cell(row, 17).Value = record.DoctorName
                    worksheet.Cell(row, 18).SetValue(record.DoctorContact)
                    worksheet.Cell(row, 19).Value = record.Clubname
                    worksheet.Cell(row, 20).Value = record.Country
                    worksheet.Cell(row, 21).Value = record.Address
                    worksheet.Cell(row, 22).Value = record.City
                    worksheet.Cell(row, 23).Value = record.Province
                    worksheet.Cell(row, 24).Value = record.Distance
                    worksheet.Cell(row, 25).Value = record.Category
                    worksheet.Cell(row, 26).Value = record.Description

                    Newtime = record.StartTime
                    worksheet.Cell(row, 27).Value = Newtime.ToString("hh:mm")

                    worksheet.Cell(row, 28).Value = record.Price
                    worksheet.Cell(row, 29).Value = record.M_reference
                    worksheet.Cell(row, 30).Value = record.Pf_reference
                    worksheet.Cell(row, 31).Value = record.RaceID

                    row += 1

                Next

                worksheet.Columns().AdjustToContents()

                Using stream
                    workbook.SaveAs(stream)
                    Dim content = stream.ToArray()
                    Return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProntoEntries_RaceDetails.xlsx")
                End Using


            End Using


            Return ("")
        End Function

        <Authorize>
        Function ExporttoExcelAddonSales(Id As Integer?)
            Dim RaceParticipants = db.Entries.Where(Function(a) a.RaceID = Id)
            Dim results = db.PartDivs.Where(Function(a) RaceParticipants.Any(Function(b) b.ParticipantID = a.ParticipantID))
            Dim MyData = results.ToList()


            Dim workbook As New XLWorkbook()
            Dim stream As New MemoryStream()

            Using workbook
                Dim worksheet = workbook.AddWorksheet("Report")


                worksheet.Cell(1, 1).Value = "First Name"
                worksheet.Cell(1, 2).Value = "Middle Name"
                worksheet.Cell(1, 3).Value = "Last Name"
                worksheet.Cell(1, 4).Value = "ID Number"
                worksheet.Cell(1, 5).Value = "Date of Birth"
                worksheet.Cell(1, 6).Value = "Gender"
                worksheet.Cell(1, 7).Value = "License Number"
                worksheet.Cell(1, 8).Value = "Email Address"
                worksheet.Cell(1, 9).Value = "Mobile"
                worksheet.Cell(1, 10).Value = "Medical Name"
                worksheet.Cell(1, 11).Value = "Medical Number"
                worksheet.Cell(1, 12).Value = "Emergency Contact"
                worksheet.Cell(1, 13).Value = "Emergency Number"
                worksheet.Cell(1, 14).Value = "Blood Type"
                worksheet.Cell(1, 15).Value = "Allergies"
                worksheet.Cell(1, 16).Value = "Additional Info"
                worksheet.Cell(1, 17).Value = "Doctor Name"
                worksheet.Cell(1, 18).Value = "Doctor Contact"
                worksheet.Cell(1, 19).Value = "Club Name"
                worksheet.Cell(1, 20).Value = "Country"
                worksheet.Cell(1, 21).Value = "Address"
                worksheet.Cell(1, 22).Value = "City"
                worksheet.Cell(1, 23).Value = "Province"
                worksheet.Cell(1, 24).Value = "Distance"
                worksheet.Cell(1, 25).Value = "Category"
                worksheet.Cell(1, 26).Value = "Description"
                worksheet.Cell(1, 27).Value = "Start Time"
                worksheet.Cell(1, 28).Value = "Price"
                worksheet.Cell(1, 29).Value = "M_reference"
                worksheet.Cell(1, 30).Value = "Pf_Reference"
                worksheet.Cell(1, 31).Value = "RaceID"

                Dim Addonheader = db.AddonItems.Where(Function(a) a.RaceID = Id).OrderBy(Function(b) b.ItemID)
                Dim i = 0

                For Each Addon In Addonheader
                    i += 1
                    worksheet.Cell(1, 31 + i).Value = Addon.Name.ToString()
                Next

                Dim row = 2
                Dim Newtime As DateTime

                For Each record In MyData
                    worksheet.Cell(row, 1).Value = record.FirstName
                    worksheet.Cell(row, 2).Value = record.MiddleNames
                    worksheet.Cell(row, 3).Value = record.LastName
                    worksheet.Cell(row, 4).SetValue(record.IDNumber)
                    worksheet.Cell(row, 5).Value = record.DOB
                    worksheet.Cell(row, 6).Value = record.Gender
                    worksheet.Cell(row, 7).Value = record.RaceNumber
                    worksheet.Cell(row, 8).Value = record.EmailAddress
                    worksheet.Cell(row, 9).Value = record.Mobile
                    worksheet.Cell(row, 10).Value = record.MedicalName
                    worksheet.Cell(row, 11).SetValue(record.MedicalNumber)
                    worksheet.Cell(row, 12).Value = record.EmergencyContact
                    worksheet.Cell(row, 13).SetValue(record.EmergencyNumber)
                    worksheet.Cell(row, 14).Value = record.BoodType
                    worksheet.Cell(row, 15).Value = record.Allergies
                    worksheet.Cell(row, 16).Value = record.AdditionalInfo
                    worksheet.Cell(row, 17).Value = record.DoctorName
                    worksheet.Cell(row, 18).SetValue(record.DoctorContact)
                    worksheet.Cell(row, 19).Value = record.Clubname
                    worksheet.Cell(row, 20).Value = record.Country
                    worksheet.Cell(row, 21).Value = record.Address
                    worksheet.Cell(row, 22).Value = record.City
                    worksheet.Cell(row, 23).Value = record.Province
                    worksheet.Cell(row, 24).Value = record.Distance
                    worksheet.Cell(row, 25).Value = record.Category
                    worksheet.Cell(row, 26).Value = record.Description

                    Newtime = record.StartTime
                    worksheet.Cell(row, 27).Value = Newtime.ToString("hh:mm")

                    worksheet.Cell(row, 28).Value = record.Price
                    worksheet.Cell(row, 29).Value = record.M_reference
                    worksheet.Cell(row, 30).Value = record.Pf_reference
                    worksheet.Cell(row, 31).Value = record.RaceID

                    Dim purchasedaddon = db.Sales.Where(Function(a) a.ParticipantID = record.ParticipantID And a.RaceID Is Nothing).OrderBy(Function(c) c.ItemID)

                    Dim j = 0

                    For Each item In purchasedaddon
                        For x = 1 To i
                            If worksheet.Cell(1, 31 + x).Value = db.AddonItems.Where(Function(a) a.ItemID = item.ItemID).Select(Function(b) b.Name).FirstOrDefault().ToString() Then
                                worksheet.Cell(row, 31 + x).Value = db.AddonOptions.Where(Function(a) a.OptionID = item.OptionID).Select(Function(b) b.Size).FirstOrDefault().ToString()
                            End If
                        Next
                    Next

                    row += 1

                Next

                worksheet.Columns().AdjustToContents()

                Using stream
                    workbook.SaveAs(stream)
                    Dim content = stream.ToArray()
                    Return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProntoEntries_RaceDetails_Addons.xlsx")
                End Using


            End Using


            Return ("")
        End Function



    End Class
End Namespace