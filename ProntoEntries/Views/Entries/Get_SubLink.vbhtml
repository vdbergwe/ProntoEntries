﻿@If ViewBag.SubOpen = True Then
    @<i> | </i>
    @Html.ActionLink("Transfer", "Create", "Vouchers", New With {.id = ViewBag.EntryID}, Nothing)
    @<i> | </i>
    @Html.ActionLink("Upgrade", "Upgrade", "Entries", New With {.id = ViewBag.EntryID}, Nothing)
End If

