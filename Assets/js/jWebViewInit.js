window.addEventListener('click', function () {
	var evnt = {};
	evnt.ControlKey = "";
	evnt.EventName = "WindowClick";
	evnt.DataSetName = "";
	evnt.EventArg = "";
	window.chrome.webview.postMessage(evnt);
});
document.addEventListener('DOMContentLoaded', function () {
    var x = document.createElement('STYLE');
    x.appendChild(document.createTextNode('::-webkit-scrollbar {width: 14px;background-color: #2e2e2e;}'));
    x.appendChild(document.createTextNode('::-webkit-scrollbar-corner {background-color: #2e2e2e;}'));
    x.appendChild(document.createTextNode('::-webkit-scrollbar-thumb {width: 14px;border-color: #2e2e2e;border-style: solid;border-size: .5px;background-color: #4d4d4d;}'));
    x.appendChild(document.createTextNode('::-webkit-scrollbar-thumb:hover {width: 14px;border-color: #2e2e2e;border-style: solid;border-size: .5px;background-color: #cccccc;}'));
    x.appendChild(document.createTextNode('::-webkit-scrollbar-track {background-color: #2e2e2e;}'));
    //x.appendChild(document.createTextNode('::-webkit-scrollbar-button {background-color: #2e2e2e; height: 16px !important;}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:horizontal:increment:end:hover {height: 16px !important;background-color: #cccccc;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_right.png);}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:horizontal:decrement:start:hover {height: 16px !important;background-color: #cccccc;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_left.png);}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:vertical:increment:end:hover {height: 16px !important;background-color: #cccccc;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_down.png);}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:vertical:decrement:start:hover {height: 16px !important;background-color: #cccccc;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_up.png);}'));

	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:horizontal:increment:end {background-color: #2e2e2e; height: 16px !important;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_right_default.png);}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:horizontal:decrement:start {background-color: #2e2e2e; height: 16px !important;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_left_default.png);}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:vertical:increment:end {background-color: #2e2e2e; height: 16px !important;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_down_default.png);}'));
	x.appendChild(document.createTextNode('::-webkit-scrollbar-button:vertical:decrement:start {background-color: #2e2e2e; height: 16px !important;background-repeat:no-repeat;background-position:center;background-image: url(http://myAncestry/img/ico_20_scrollbar_up_default.png);}'));
    x.appendChild(document.createTextNode('#BannerRegion {display:none;}'));
    x.appendChild(document.createTextNode('#HeaderRegion {display:none;}'));
    document.head.appendChild(x);
});
window.MyAncestryAPI = {
	ctlType: "",
	ctlKey: "",
	ctlDataSet: "",
	selectedIndex: -1,
	selectedText: "",
	itemType: "none",
	itemCount: 0,
	itemVisibleCount: 0,
	filter: "",
	columnCount: 0,
	data: chrome.webview.hostObjects.sync.DataMgr,
	metadata: {},
	setControlFocus: function () {document.body.className = 'webcontrol hasfocus'; },
	removeControlFocus: function () { document.body.className = 'webcontrol'; },
	addEventHandlers: function (tag) {
		var tbody
		tbody = document.getElementById("dataset");
		Array.from(tbody.getElementsByTagName(tag)).forEach(function (item) {
			item.addEventListener('click', function () {
				document.querySelectorAll('.selected').forEach(function (sitem) { sitem.className = ""; });
				this.className = 'selected';
				window.MyAncestryAPI.selectedIndex = this.getAttribute("data-rid");
				try {
					var evnt = {};
					evnt.ControlKey = window.MyAncestryAPI.ctlKey;
					evnt.EventName = "ItemSelected";
					evnt.DataSetName = window.MyAncestryAPI.ctlDataSet;
					evnt.EventArg = this.getAttribute("data-rid");
					window.chrome.webview.postMessage(evnt);
				} catch (e) {
				}
			});
		});
	},
	SyncItem: function (idx) {
		var i, j;
		if (idx == -1) { idx = 1; }
		this.dataRow = JSON.parse(this.data.getDataRowAsJson(this.ctlDataSet, idx));
		for (i = 1; i <= this.metadata.Rows; i++) {
			this.col = i - 1;
			document.querySelectorAll("[data-bind='" + i + "']").forEach(function (sitem) {
				sitem.innerText = window.MyAncestryAPI.dataRow[window.MyAncestryAPI.col];
			});
		}
    },
	createDataSync: function (dataSetName) {
		this.ctlType = "DataSync";
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
		this.selectionChanged = function (msg) {
			var obj = JSON.parse(msg);
			if (obj.DataSetName == this.ctlDataSet) {
				window.MyAncestryAPI.SyncItem(obj.EventArg);
			}
		};
		this.metadata = JSON.parse(this.data.getMetaDataAsJson(dataSetName));
		this.SyncItem(this.metadata.RowSelected);
	},
	createList: function (dataSetName) {
		this.ctlType = "List";
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
		var htm = "";
		var i, j;
		this.metadata = JSON.parse(this.data.getMetaDataAsJson(dataSetName));
		htm += "<TABLE class='List'>";
		htm += "<TBODY id='dataset'>";
		for (i = 1; i <= this.metadata.Rows; i++) {
			htm += "<TR data-rid=" + i + ">";
			var dataRow = JSON.parse(this.data.getDataRowAsJson(this.ctlDataSet, i));
			htm += "<TD>" + dataRow[0] + "</TD>";
			htm += "</TR>";
		}
		htm += "</TBODY></TABLE>";
		body.innerHTML = htm;
		this.addEventHandlers("tr");
	},
	createColumnList: function (dataSetName) {
		this.ctlType = "ColumnList";
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
		var htm = "";
		var i,j;
		this.metadata = JSON.parse(this.data.getMetaDataAsJson(dataSetName));
		htm += "<TABLE class='ColumnList'><THEAD>";
		for (i = 0; i < this.metadata.Columns.length; i++) {
			if (this.metadata.Columns[i].Visible==true) {
				htm += "<TH>" + this.metadata.Columns[i].Name + "</TH>";
			}
        }
		htm+="</THEAD><TBODY id='dataset'>";
		for (i = 1; i <= this.metadata.Rows; i++) {
			htm += "<TR data-rid="+i+">";
			var dataRow = JSON.parse(this.data.getDataRowAsJson(this.ctlDataSet, i));
			//array[0] is always the unique rowid of the datarow
			for (j = 0; j < this.metadata.Columns.length; j++) {
			if (this.metadata.Columns[j].Visible ==true) {
					htm += "<TD>" + dataRow[j] + "</TD>";
				}
			}
			htm += "</TR>";
		}
		htm += "</TBODY></TABLE>";
		body.innerHTML = htm;
		this.addEventHandlers("tr");
	},
	createTable: function (dataSetName) {
		this.ctlType = "Table";
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
	},
	createTree: function (dataSetName) {
		this.ctlType = "Tree";
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
	},
	createTimeline: function (dataSetName) {
		this.ctlType = "Timeline";
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
	},
	createControl: function (controlType, dataSetName) {
		this.ctlType = controlType;
		this.ctlDataSet = dataSetName;
		var body = document.body;
		body.className = "webcontrol";
        switch (controlType) {
			case "List":
				this.createList(dataSetName);
				break;
			case "ColumnList":
				this.createColumnList(dataSetName);
				break;
			case "Table":
				this.createTable(dataSetName);
				break;
			case "Tree":
				this.createTree(dataSetName);
				break;
			case "TimeLine":
				this.createTimeline(dataSetName);
				break;
			case "DataSync":
				this.createDataSync(dataSetName);
            default:
        }
	},
	applyFilter: function (filterValue) {
		var filter, ol, li, i, txtValue;
		filter = filterValue.toUpperCase();
		ol = document.getElementById("dataset");
		li = ol.getElementsByTagName('tr');
		for (i = 0; i < li.length; i++) {
			txtValue = li[i].innerText
			if (txtValue.toUpperCase().indexOf(filter) > -1) {
				li[i].style.display = "";
			} else {
				li[i].style.display = "none";
			}
		}
		return true;
	},
	clearFilter: function () {
		var ol, li, i;
		ol = document.getElementById("dataset");
		li = ol.getElementsByTagName('tr');
		for (i = 0; i < li.length; i++) {
			li[i].style.display = "";
		}
		return true;
	},
	selectItem: function (itemIdx) {
		console.log("SelectItem: o:" + window.MyAncestryAPI.selectedIndex + "== n:" + itemIdx);

		if (window.MyAncestryAPI.selectedIndex != itemIdx) {
			window.MyAncestryAPI.selectedIndex = itemIdx;
			document.querySelectorAll('.selected').forEach(function (sitem) { sitem.className = ""; });
			console.log("cleared selected");
			document.querySelectorAll("tr[data-rid='" + itemIdx + "']").forEach(function (sitem) { sitem.className = "selected"; });
		}
	},
	selectionChanged: function (msg) {
		var obj = JSON.parse(msg);
		if (obj.DataSetName == this.ctlDataSet) {
			this.selectItem(obj.EventArg);
        }
    },
	getSelectedItem:function () {
		var items = document.querySelectorAll('.selected');
		if (items.length > 0) {
			return items[0].getAttribute("data-rid") + '|' + items[0].innerText + '|';
		} else {
			return '-1||';
		}
	}
};

