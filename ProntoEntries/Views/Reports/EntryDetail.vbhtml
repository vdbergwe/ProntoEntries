@ModelType ProntoEntries.Participant
@Code
    ViewData("Title") = "EntryDetail"
End Code


<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Entry Details</h2>
        <div>
            <ul>
                <li>
                    @*@Html.ActionLink("Back To Search", "Index", New With {.RaceID = ViewBag.RaceID, .SearchValue = ViewBag.SearchValue})*@
                    @Html.ActionLink("Back To Search", "Index", "Reports", New With {.RaceID = ViewBag.RaceID, .SearchValue = ViewBag.SearchValue}, Nothing)
                </li>
            </ul>
        </div>
    </div>
    <hr />
    <div Class="ReportContent">
        <div>
            @* Entry Details *@
            <div class="dashboardcontainer">
                <div class="reportitem">
                    <h3>Entry Detail</h3>
                    <Table Class="tablereport">
                        <tr>
                            <th>
                                Distance
                            </th>
                            <th>
                                Category
                            </th>
                            <th>
                                Collection Point
                            </th>
                        </tr>
                        <tr>
                            <td>
                                @ViewBag.Distance
                            </td>
                            <td>
                                @ViewBag.Category
                            </td>
                            <td>
                                @ViewBag.Collection
                            </td>
                        </tr>
                    </Table>
                </div>
                @* Add-on Details *@

                <div class="reportitem">
                    <h3>Selected Add-ons</h3>
                    <table class="tablereport">
                        @Html.Action("ViewAddOns", "AddonOptions", New With {.Id = ViewBag.Mref, .ParticipantID = ViewBag.ParticipantID})
                        <!--<tr>
                            <th>
                                Name
                            </th>
                            <th>
                                Option
                            </th>
                        </tr>
                        <tr>
                            <td>-->
                                @*@Html.DisplayFor(Function(model) model.MedicalName)*@
                            <!--</td>
                            <td>-->
                                @*@Html.DisplayFor(Function(model) model.MedicalNumber)*@
                            <!--</td>-->
                    </table>
                </div>
            </div>
        </div>
        <div>
            @* Payment Details *@
            <div class="dashboardcontainer">
                <div class="reportitem">
                    <h3>Payment</h3>
                    <Table Class="tablereport">
                        <tr>
                            <th>
                                PF Reference
                            </th>
                            <th>
                                M Reference
                            </th>
                            <th>
                                Amount Gross
                            </th>
                        </tr>
                        <tr>
                            <td>
                                @ViewBag.Pfref
                            </td>
                            <td>
                                @ViewBag.Mref
                            </td>
                            <td>
                                @ViewBag.Amount
                            </td>
                        </tr>
                    </Table>
                </div>               
            </div>
        </div>
        </div>
        <div class="dashboardcontainer">
            <div class="reportitem">
                <h3>Personal</h3>
                <Table Class="tablereport">
                    <tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.FirstName)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.MiddleNames)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.LastName)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.IDNumber)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.EmailAddress)
                        </th>
                    </tr>
                    <tr>
                        <td>
                            @Html.DisplayFor(Function(model) model.FirstName)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.MiddleNames)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.LastName)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.IDNumber)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.EmailAddress)
                        </td>
                    </tr>
                </Table>
            </div>
            <div class="reportitem">
                <h3>Medical</h3>
                <table class="tablereport">
                    <tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.MedicalName)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.MedicalNumber)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.EmergencyContact)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.EmergencyNumber)
                        </th>
                    </tr>
                    <tr>
                        <td>
                            @Html.DisplayFor(Function(model) model.MedicalName)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.MedicalNumber)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.EmergencyContact)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.EmergencyNumber)
                        </td>

                    </tr>
                    <tr>
                        <th>
                            <hr />
                        </th>
                        <th>
                            <hr />
                        </th>
                        <th>
                            <hr />
                        </th>
                    </tr>
                    <tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.BoodType)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.Allergies)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.AdditionalInfo)
                        </th>
                    </tr>
                    <tr>
                        <td>
                            @Html.DisplayFor(Function(model) model.BoodType)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.Allergies)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.AdditionalInfo)
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="dashboardcontainer">
            <div class="reportitem">
                <h3>Club</h3>
                <table class="tablereport">
                    <tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.Clubname)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.RaceNumber)
                        </th>
                    </tr>
                    <tr>
                        <td>
                            @Html.DisplayFor(Function(model) model.Clubname)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.RaceNumber)
                        </td>
                    </tr>
                </table>
            </div>
            <div class="reportitem">
                <h3>Doctor</h3>
                <table class="tablereport">
                    <tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.DoctorName)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.DoctorContact)
                        </th>
                    </tr>
                    <tr>
                        <td>
                            @Html.DisplayFor(Function(model) model.DoctorName)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.DoctorContact)
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="dashboardcontainer">
            <div class="reportitem">
                <h3>Address</h3>
                <table class="tablereport">
                    <tr>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.Country)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.Address)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.City)
                        </th>
                        <th>
                            @Html.DisplayNameFor(Function(model) model.Province)
                        </th>
                    </tr>
                    <tr>
                        <td>
                            @Html.DisplayFor(Function(model) model.Country)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.Address)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.City)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(model) model.Province)
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</div>