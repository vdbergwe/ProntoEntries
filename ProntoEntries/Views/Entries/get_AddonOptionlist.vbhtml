@ModelType IEnumerable(Of ProntoEntries.AddonOption)


@For Each item In Model
    @<div>
    <ul>
        <li>
            @Html.DisplayFor(Function(modelItem) item.Size)
        </li>
        R
        <li>
            @Html.DisplayFor(Function(modelItem) item.Amount)
        </li>
        <li>
            @Html.ActionLink("Select", "VerifyEntry", "Entries", New With {.id = ViewBag.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID, .OptionID1 = item.OptionID, .ItemID = item.ItemID}, Nothing)
        </li>
    </ul>
</div>





Next
