@ModelType IEnumerable(Of ProntoEntries.AddonOption)

@If Model.Count() = 0 Then
    @<div>
        <p>
            Not Available
        </p>
    </div>
End If


@For Each item In Model
    @*@<div>
            @Html.DisplayFor(Function(modelItem) item.Size)
            - R
            @Html.DisplayFor(Function(modelItem) item.Amount)
            <ul>
                <li>
                    @Html.ActionLink("Select", "VerifyEntry", "Entries", New With {.id = ViewBag.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID, .OptionID1 = item.OptionID, .ItemID = item.ItemID}, Nothing)
                </li>
            </ul>
        </div>*@

    @If item.Amount = 0 Then
        @<div class="addonoptionlist">
            @Html.ActionLink(item.Size.ToString(), "VerifyEntry", "Entries", New With {.id = ViewBag.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID, .OptionID1 = item.OptionID, .ItemID = item.ItemID}, Nothing)
        </div>
    Else
        @<div class="addonoptionlist">
            @Html.ActionLink(item.Size.ToString() + " - R" + item.Amount.ToString(), "VerifyEntry", "Entries", New With {.id = ViewBag.ParticipantID, .RaceID1 = ViewBag.RaceID, .DivisionID1 = ViewBag.DivisionID, .OptionID1 = item.OptionID, .ItemID = item.ItemID}, Nothing)
        </div>
    End If

Next
