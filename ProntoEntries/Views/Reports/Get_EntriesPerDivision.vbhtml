@ModelType IEnumerable(Of ProntoEntries.Division)

<p>
    Total Per Division:
</p>

<div Class="ReportContent">
    <Table Class="table">
        <tr>
            <th>
                Description
            </th>
            <th>
                Total
            </th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Description)
                </td>
                <td>
                    @Html.Action("Get_DivisionTotal", New With {.Id = item.DivisionID})
                </td>
            </tr>
        Next
    </Table>
</div>
