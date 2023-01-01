@ModelType IEnumerable(Of ProntoEntries.AddonItem)

@Code
    ViewData("Title") = "Verify Entry"
End Code

<style>
        .backing {
            background: linear-gradient( rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5) ), url(@ViewBag.Background) no-repeat;
            height: 100%;
        }
        .logobox {
            content: url(@ViewBag.OrgImage);
            height: 100%;
            width: auto;
        }
</style>

<meta name="viewport" content="width=device-width" />
<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Verify Entry - @ViewBag.RaceName</h2>
        @Html.ActionLink("Change Event", "Index", "RaceEvents")
    </div>
    <hr />
    @If ViewBag.DivisionCheck = 0 Then
        @<p>No Suitable Age Class Available for the distance selected</p>
    Else
        @If ViewBag.Required > 0 Then
            @<div class="warningbanner">
                Required Item Selection Outstanding
            </div>
        End If
        @If Model.Count > 0 Then
            @<Table Class="table">
                <tr>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Name)
                    </th>
                    <th>
                        @Html.DisplayNameFor(Function(model) model.Description)
                    </th>
                    <th>
                        Required
                    </th>
                    <th>
                        Options
                    </th>

                </tr>

                @For Each item In Model
                    @<tr>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Name)
                        </td>
                        <td>
                            @Html.DisplayFor(Function(modelItem) item.Description)
                        </td>
                        <td>
                            @If item.Required = True Then
                                @<p>
                                    Yes
                                </p>
                            End If
                        </td>
                        <td>
                            @Html.Action("get_AddonOptionList", New With {.Id = item.ItemID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID, .OptionID1 = ViewBag.OptionID, .ParticipantID = ViewBag.ParticipantID})
                        </td>
                    </tr>
                Next

            </Table>
            @<hr />
        End If

        @<div>
            <h3 style="text-align: center; color: #FFF; text-transform: uppercase"> Terms And Conditions</h3>
            <hr />
            <p>
                @ViewBag.TandC
            </p>
            <h3 style="text-align: center; color: #FFF; text-transform: uppercase"> Indemnity</h3>
            <hr />
            <p>
                @ViewBag.Indemnity
            </p>
            @If ViewBag.Required > 0 Then

            Else
                @<div Class="button-group">
                    <div Class="linkbutton">
                        @*<button onclick="confirmaction()">Accept</button>*@
                        @Html.ActionLink("Accept", "Addtocart", "Entries", New With {.Id = ViewBag.ParticipantID, .RaceID = ViewBag.RaceID, .Distance = ViewBag.DivisionID}, New With {.onclick = " return confirm('Please confirm all required Addons were selected.  By clicking on OK, you acknowledge acceptance of the indemnity waiver and Terms and Conditions clauses.');"})
                    </div>
                </div>
            End If

        </div>
    End If

</div>

