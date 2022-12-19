@ModelType IEnumerable(Of ProntoEntries.AddonOption)
@Code
ViewData("Title") = "Index"
End Code

<table>
    <tr>
        <th>
            Item
        </th>
        <th style="padding: 0 0 0 100px;">
            Option
        </th>
    </tr>

@For Each item In Model
    @<tr>
        <td>
            @Html.Action("Get_ItemName", New With {.Id = item.ItemID})
        </td>
        <td style="padding: 0 0 0 100px;">
            @Html.DisplayFor(Function(modelItem) item.Size)
        </td>
        <td>
            @If item.Size = "LVCC club house in Nelspruit" Then
                @<img src="~/Content/QRCodes/LVCCQRCode.png" width="116" height="200" style="width: 220px; margin: 0 0 0 10px" />

            End If
            @If item.Size = "Oosterlijn School At eMgwenya" Then
                @<img src="~/Content/QRCodes/WatervalBovenQRCode.png" width="116" height="200" style="width: 220px; margin: 0 0 0 10px" />
            End If
            @*<img src="@Url.Content(ViewBag.Background)" style="width: 220px; margin: 0 0 0 10px" />*@
        </td>
    </tr>
Next

</table>
