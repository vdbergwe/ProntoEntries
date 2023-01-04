@ModelType IEnumerable(Of ProntoEntries.AddonOption)

<p>
    Total Per Add-on:
</p>

<div Class="ReportContent">
    <Table Class="table">
        <tr>
            <th>
                Item
            </th>
            <th>
                Size
            </th>
            <th>
                Total
            </th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.Action("Get_ItemName", "AddonOptions", New With {.Id = item.ItemID})
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Size)
                </td>
                <td>
                    @Html.Action("Get_OptionCount", New With {.Id = item.OptionID})
                </td>
            </tr>
        Next
    </Table>
</div>
