@If ViewBag.SubOpen = True Then
    @<i> | </i>
    @Html.ActionLink("Transfer", "Transfer", "Entries", New With {.id = ViewBag.EntryID}, Nothing)

    @If ViewBag.Upgrade = True Then

    Else
        @<i> | </i>
        @Html.ActionLink("Upgrade", "Upgrade", "Entries", New With {.id = ViewBag.EntryID}, Nothing)
    End If

End If

