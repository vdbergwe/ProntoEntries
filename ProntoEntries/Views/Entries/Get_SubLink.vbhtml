@If ViewBag.SubOpen = True Then
    @<i> | </i>
    @Html.ActionLink("Transfer", "Create", "Vouchers", New With {.id = ViewBag.EntryID}, Nothing)
End If

