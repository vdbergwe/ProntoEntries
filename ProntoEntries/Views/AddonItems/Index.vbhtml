@ModelType IEnumerable(Of ProntoEntries.AddonItem)
@Code
    ViewData("Title") = "Index"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Addon Items</h2>
        @Html.ActionLink("Create New", "Create")
    </div>
    <hr />
    <form action="" method="get" style="text-align:center">
        <div class="DropdownDashboard">
            <div Class="DropdownDashboard item" , id="eventselect">
                <p>Event Name</p>
                @Html.DropDownList("RaceID", Nothing, htmlAttributes:=New With {.class = "form-controlDDL"}, optionLabel:="Select Event")
            </div>
            <input type="submit" name="submit" />
            @*<div class="DropdownSearches item">
                    <p>Search Value</p>
                    <input type="text" name="SearchValue" id="SearchValue" class="form-control" placeholder="@ViewBag.SearchText" />
                </div>*@
        </div>
    </form>
    <hr />


    @If ViewBag.SelectedRace IsNot Nothing Then
        @<Table Class="table">
            <tr>
                <th>
                    @Html.DisplayNameFor(Function(model) model.RaceID)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Name)
                </th>
                <th>
                    @Html.DisplayNameFor(Function(model) model.Description)
                </th>
            </tr>

            @For Each item In Model
                @<tr>
                    <td>
                        @Html.Action("get_RaceName", New With {.Id = item.RaceID})
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Name)
                    </td>
                    <td>
                        @Html.DisplayFor(Function(modelItem) item.Description)
                    </td>
                    <td>
                        @*@Html.ActionLink("Add Option", "Create", "AddonOptions", New With {.id = item.ItemID}, Nothing) |*@
                        @Html.ActionLink("Edit", "Edit", New With {.id = item.ItemID}) |
                        @Html.ActionLink("Delete", "Delete", New With {.id = item.ItemID}) |
                        @Html.ActionLink("Modify", "IndexPartial", "AddonOptions", New With {.Id = item.ItemID}, Nothing)

                    </td>
                </tr>
                @*@Html.Action("IndexPartial", "AddonOptions", New With {.Id = item.ItemID})*@
            Next

        </Table>
    Else
        @<div style="text-align:center">
            <p>Please Select Event</p>
        </div>
    End If


</div>
