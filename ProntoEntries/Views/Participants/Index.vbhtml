@ModelType IEnumerable(Of ProntoEntries.Participant)
@Code
    ViewData("Title") = "Participants"
End Code

<div class="orgcontainer">
    <div class="titlediv">
        <h2>Participants</h2>
        @Html.ActionLink("Create New", "Create")

    </div>

    <hr />
    <table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.FirstName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.LastName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.IDNumber)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceNumber)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.EmailAddress)
            </th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.FirstName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.LastName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.IDNumber)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceNumber)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.EmailAddress)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.ParticipantID}) @*|
                    @Html.ActionLink("Details", "Details", New With {.id = item.ParticipantID}) |*@
                    @*@Html.ActionLink("Delete", "Delete", New With {.id = item.ParticipantID })*@
                </td>
            </tr>
        Next

    </table>
</div>
