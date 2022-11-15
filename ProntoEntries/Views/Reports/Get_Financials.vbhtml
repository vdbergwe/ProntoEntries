@ModelType IEnumerable(Of ProntoEntries.Division)

<p>
    Amount Per Division:
</p>

<div Class="ReportContent">
    <Table Class="table">
        <tr>
            <th>
                Description
            </th>
            <th>
                Total Amount
            </th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Description)
                </td>
                <td>
                    R
                    @Html.Action("Get_DivisionTotalAmount", New With {.Id = item.DivisionID})
                </td>
            </tr>
        Next
    </Table>
</div>
