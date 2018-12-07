function downloadExecl(data, ExcelName, type) {

    var keys = Object.keys(data[0]);
    var firstRow = {};
    keys.forEach(function (item) {
        firstRow[item] = item;
    });
    data.unshift(firstRow);

    var content = {};

    // 鎶妀son鏍煎紡鐨勬暟鎹浆涓篹xcel鐨勮鍒楀舰寮�
    var sheetsData = data.map(function (item, rowIndex) {
        return keys.map(function (key, columnIndex) {
            return Object.assign({}, {
                value: item[key],
                position: (columnIndex > 25 ? getCharCol(columnIndex) : String.fromCharCode(65 + columnIndex)) + (rowIndex + 1),
            });
        });
    }).reduce(function (prev, next) {
        return prev.concat(next);
    });

    sheetsData.forEach(function (item, index) {
        content[item.position] = { v: item.value };
    });

    //璁剧疆鍖哄煙,姣斿琛ㄦ牸浠嶢1鍒癉10,SheetNames:鏍囬锛�
    var coordinate = Object.keys(content);
    var workBook = {
        SheetNames: ["Sheet1"],
        Sheets: {
            "Sheet1": Object.assign({}, content, { "!ref": coordinate[0] + ":" + coordinate[coordinate.length - 1] }),
        }
    };
    //杩欓噷鐨勬暟鎹槸鐢ㄦ潵瀹氫箟瀵煎嚭鐨勬牸寮忕被鍨�
    var excelData = XLSX.write(workBook, { bookType: "xlsx", bookSST: false, type: "binary" });
    var blob = new Blob([string2ArrayBuffer(excelData)], { type: "" });
    saveAs(blob, ExcelName + ".xlsx");
}
//瀛楃涓茶浆瀛楃娴�
function string2ArrayBuffer(s) {
    var buf = new ArrayBuffer(s.length);
    var view = new Uint8Array(buf);
    for (var i = 0; i != s.length; ++i) view[i] = s.charCodeAt(i) & 0xFF;
    return buf;
}
// 灏嗘寚瀹氱殑鑷劧鏁拌浆鎹负26杩涘埗琛ㄧず銆傛槧灏勫叧绯伙細[0-25] -> [A-Z]銆�
function getCharCol(n) {
    let temCol = "",
        s = "",
        m = 0
    while (n > 0) {
        m = n % 26 + 1
        s = String.fromCharCode(m + 64) + s
        n = (n - m) / 26
    }
    return s
}