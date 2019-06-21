function JSonStringBuilder() {
    this.builder = "";
}

JSonStringBuilder.prototype.Begin = function () {
    this.builder += "{";
}

JSonStringBuilder.prototype.End = function () {
    this.builder = this.builder.substr(0, this.builder.length - 1);
  
    this.builder += "}";
}

//不管传入的data是什么类型，都作为字符串类型处理，所以数据需要加上双引号。
JSonStringBuilder.prototype.Add = function (key, data) {
    this.builder += "\"" + key + "\":" + "\"" + data + "\",";
}

JSonStringBuilder.prototype.toString = function () {
    return this.builder;
}
