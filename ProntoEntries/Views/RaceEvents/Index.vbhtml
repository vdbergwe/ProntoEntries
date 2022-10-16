@ModelType IEnumerable(Of ProntoEntries.RaceEvent)
@Code
    ViewData("Title") = "Upcoming Events"
End Code

<div class="orgcontainer create">
    <div class="titlediv">
        <h2>Upcoming Events</h2>
        <div>
            <ul>
                <li>
                    @Html.ActionLink("New Division", "Create", "Divisions")
                </li>
                <li>
                    @Html.ActionLink("New Event", "Create")
                </li>
            </ul>
           
           
        </div>

    </div>


    @*<p>
            @Html.ActionLink("Create New", "Create")
        </p>*@
    <hr />
    <div class="raceflex">
        @For Each item In Model
            @<div class="raceflexitem">
                @If (item.Image.Length > 0) Then
                    @<img src="@Url.Content(item.Image)" class="eventimg" />
                End If
                <ul class="eventselecter">
                    <li>
                        @Html.DisplayFor(Function(modelItem) item.RaceName)
                    </li>
                    <li>
                        <i class="fa-solid fa-calendar-days"></i>
                        @item.RaceDate.Value.ToString("dddd, dd MMMM yyyy")
                        @*@Html.DisplayFor(Function(modelItem) item.RaceDate)*@
                    </li>
                    <li>
                        <i class="fa-solid fa-location-dot"></i>
                        @Html.DisplayFor(Function(modelItem) item.Address)
                    </li>
                    <li>
                        <i class="fa-solid fa-flag-checkered"></i>
                        @Html.Action("get_devdistance", New With {.Id = item.RaceID})
                    </li>
                    <li>
                        <i class="fa-solid fa-hand-holding-dollar"></i>
                        @Html.Action("get_devprice", New With {.Id = item.RaceID})
                    </li>
                    <li>
                        @Html.DisplayFor(Function(modelItem) item.RaceDescription)
                    </li>
                </ul>
                <hr />
                <div class="button-group">
                    <div class="linkbutton">
                                @Html.ActionLink("View Event", "Details", New With {.id = item.RaceID})
                    </div>
                </div>
            </div>
        Next
    </div>
</div>

@*<table class="table">
        <tr>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceName)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceDescription)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceDate)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.RaceType)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Coordinates)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Address)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.City)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Province)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.AdminCharge)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.OrgID)
            </th>
            <th>
                @Html.DisplayNameFor(Function(model) model.Image)
            </th>
            <th></th>
        </tr>

        @For Each item In Model
            @<tr>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceDescription)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceDate)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.RaceType)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Coordinates)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Address)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.City)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Province)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.AdminCharge)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.OrgID)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) item.Image)
                </td>
                <td>
                    @Html.ActionLink("Edit", "Edit", New With {.id = item.RaceID}) |
                    @Html.ActionLink("Details", "Details", New With {.id = item.RaceID}) |
                    @Html.ActionLink("Delete", "Delete", New With {.id = item.RaceID})
                </td>
            </tr>
        Next

    </table>*@
