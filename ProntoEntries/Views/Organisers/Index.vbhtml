@ModelType IEnumerable(Of ProntoEntries.Organiser)
@Code
    ViewData("Title") = "Organization List"
End Code

<div class="orgcontainer">

    <div class="titlediv">
        <h2>Organization List</h2>
        @Html.ActionLink("Create New", "Create")
    </div>

    <hr />
    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrgName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrgEmail)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrgTel)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrgWebsite)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrgVatNumber)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Image)
            </th>
            <th></th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrgName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrgEmail)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrgTel)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrgWebsite)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrgVatNumber)
                </td>
                <td>
                    @If (item.Image.Length > 0) Then
                        @<img src="@Url.Content(item.Image)" style="width: 100px" />
                    End If
                    @*@Html.DisplayFor(Function(modelItem) item.Image)*@
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.OrgID}) |
                    @*@Html.ActionLink("Details", "Details", New With {.id = item.OrgID}) |*@
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.OrgID})
                </td>
            </tr>
        Next

    </table>

</div>

