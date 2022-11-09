@ModelType IEnumerable(Of ProntoEntries.AddonItem)

@Code
    ViewData("Title") = "Verify Entry"
End Code

@*<style>
    .backing {
        background: linear-gradient( rgba(0, 0, 0, 0.5), rgba(0, 0, 0, 0.5) ), url(@ViewBag.Background) no-repeat;
        height: 100%;
    }
    .logobox {
        content: url(@ViewBag.OrgImage);
        height: 100%;
        width: auto;
    }
</style>*@

<meta name="viewport" content="width=device-width" />
<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Verify Entry - @ViewBag.RaceName</h2>
        @Html.ActionLink("Change Event", "Index", "RaceEvents")
    </div>
    <hr />

    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.Name)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Description)
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
                    @*@Html.DropDownList("OptionID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL", .id = ViewBag.ElementID, .name = ViewBag.ElementID}, optionLabel:="Select Option")*@
                    @Html.Action("get_AddonOptionList", New With {.Id = item.ItemID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID, .OptionID1 = ViewBag.OptionID, .ParticipantID = ViewBag.ParticipantID})
                </td>

                @*<td>
                    @If (ViewBag.DivisionSelect > 0) Then
                        @Html.ActionLink("Enter Event", "VerifyEntry", "Entries", New With {.id = item.ParticipantID, .RaceID = ViewBag.RaceID, .DivisionID = ViewBag.DivisionSelect}, Nothing)
                        @Html.ActionLink("Add to Cart", "Addtocart", "Entries", New With {.id = item.ParticipantID, .RaceID = ViewBag.RaceID, .DivisionID = ViewBag.DivisionSelect}, Nothing)
                    End If
                </td>*@
            </tr>
        Next

    </table>

    @*<div>
        <div class="ParticipantSelection">
            <div>
                @Html.Action("ViewParticipants", "Participants", New With {.Id = ViewBag.RaceID, .DivisionSelect = ViewBag.DivisionSelect})
            </div>
        </div>

        <div Class="form-group">
                <div Class="col-md-offset-2 col-md-10">
                    <input type="submit" value="Create" Class="btn btn-default" />
                </div>
            </div>
    </div>*@

</div>


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
    <script>
        $("#divisionselect").change(function () {
            var did = $("#divisionselect option:selected").val();
            window.location.replace("?DivisionSelect=" + did);
        });
    </script>
End Section