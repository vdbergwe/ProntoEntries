@ModelType IEnumerable(Of ProntoEntries.Division)
@Code
    ViewData("Title") = "Index"
End Code

@*<h2>Index</h2>

<p>
    @Html.ActionLink("Create New", "Create")
</p>*@
<table class="table">
    <tr>
        @*<th>
            @Html.DisplayNameFor(Function(model) model.Category)
        </th>*@
        <th>
            @Html.DisplayNameFor(Function(model) model.Distance)
        </th>
        @*<th>
                @Html.DisplayNameFor(Function(model) model.Description)
            </th>*@
        <th>
            @Html.DisplayNameFor(Function(model) model.StartTime)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Price)
        </th>
        @*<th>
                @Html.DisplayNameFor(Function(model) model.RaceID)
            </th>*@
        <th></th>
    </tr>

    @For Each item In ViewBag.Distance
        @<tr>
            @*<td>
                @Html.DisplayFor(Function(modelItem) item.Category)
            </td>*@
            <td>
                @Html.DisplayFor(Function(modelItem) item)
            </td>
            @*<td>
                    @Html.DisplayFor(Function(modelItem) item.Description)
                </td>*@
         <td>
             @Html.Action("Get_StartTime", New With {.Id = item, .RaceID = ViewBag.RaceID})

             @*@Html.DisplayFor(Function(modelItem) item.StartTime)*@
         </td>
         <td>
             R
             @Html.Action("Get_Amount", New With {.Id = item, .RaceID = ViewBag.RaceID})

             @*@Html.DisplayFor(Function(modelItem) item.Price)*@
         </td>
            @*<td>
                    @Html.DisplayFor(Function(modelItem) item.RaceID)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.DivisionID }) |
                    @Html.ActionLink("Details", "Details", New With {.id = item.DivisionID }) |
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.DivisionID })
                </td>*@
        </tr>
    Next

</table>
