@ModelType IEnumerable(Of ProntoEntries.AddonOption)

<p>
    Amount Per Add-on:
</p>

<div Class="ReportContent">
    <Table Class="table">
        <tr>
            <th>
                Add-on Item
            </th>
            <th>
                Size
            </th>
            <th>
                Total Amount
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
                    R
                    @Html.Action("Get_AddonTotalAmount", New With {.Id = item.OptionID})
                </td>
            </tr>
        Next
    </Table>
</div>
