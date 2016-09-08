$(function () {
    var JPlaceHolder = {
        //检测
        _check: function () {
            return 'placeholder' in document.createElement('input');
        },
        //初始化
        init: function () {
            if (!this._check()) {
                this.fix();
            }
        },
        //修复
        fix: function () {
            jQuery(':input[placeholder]').each(function (index, element) {
                var self = $(this), txt = self.attr('placeholder');
                self.wrap($('<div></div>').css({ position: 'relative', zoom: '1', border: 'none', background: 'none', padding: 'none', margin: 'none' }));
                var pos = self.position(), h = self.outerHeight(true), paddingleft = self.css('padding-left');
                var holder = $('<span></span>').text(txt).css({ position: 'absolute', left: pos.left, top: pos.top + "px", height: h, lineHeight: h + "px", paddingLeft: paddingleft, color: '#cdcdcd' }).appendTo(self.parent());
                self.focusin(function (e) {
                    holder.hide();
                }).focusout(function (e) {
                    if (!self.val()) {
                        holder.show();
                    }
                });
                holder.click(function (e) {
                    holder.hide();
                    self.focus();
                });
            });
        }
    };
    if (window.addEventListener) {
        document.body.addEventListener("touchstart", function () { }, false);
    }
    var datas_show = $("#datas_show");
    var Router = Backbone.Router.extend({
        initialize: function () {
            if (window.localStorage) {
                localStorage.removeItem("VINCode");
            }
        },
        routes: {
            "": "home", // 首页
            "brandModelList/:Code": "brandModelList", // 品牌型号列表页
            "brandModelListDetails/:manufacturerCode/:modelName": "brandModelListDetails", // 型号详情页
            "carDetails/:LyId": "carDetails", // 车子详情页
            "maintain/:WVMID": "maintainInstruction",// 保养信息
            "products/:LyId/:MaintenanceCode": "productsInformation" // 保养得产品信息
        },
        // 首页
        home: function () {
            datas_show.html(_.template($("#home").html())); // 渲染首页search框部分
            var search_result = $("#search_result");
            JPlaceHolder.init();
            var vin = $("#vin");
            if (window.localStorage) {
                var vinCode = localStorage.getItem("VINCode");
                if (vinCode) {
                    vin.val(vinCode);
                }
            }
            // 渲染品牌图标方法
            var renderBrand = function () {
                $.getJSON("/api/Vehicles/GetBrands", function (d) {
                    var brandDatas = d;
                    var tp = _.template($("#home_brand").html());
                    search_result.html(tp({ brandDatas: brandDatas }));
                });
            }
            // 点返回到首页时判断是否存在VIN的Cookie渲染查询结果的模板
            if ($.cookie("VINCookie") != "[object Object]" && $.cookie("VINCookie") != undefined) {
                var vinC = $.cookie("VINCookie");
                $.getJSON("/api/Vehicles/Vin/" + encodeURI(vinC), function (datas) {
                    var l = datas.length;
                    var responseFirst = datas[0].Models;
                    if (l > 1) {
                        for (var i = 1; i < l; i++) {
                            responseFirst = responseFirst.concat(datas[i].Models);
                        }
                    }
                    var vinDatas = responseFirst;
                    var template = _.template($("#vin_result").html());
                    search_result.html(template({ vinDatas: vinDatas }));
                    $("#back_prev,#c_home").on("click", function () {
                        $.cookie("VINCookie", { expires: -1 });
                        renderBrand();
                    });
                });
            }
                // 刷新页面时不存在VIN的Cookie直接渲染品牌模板
            else {
                renderBrand();
            }
            var ww;
            var search = $(".search");
            var clearText = $(".clearText");
            $(window).resize(function () {
                ww = $(this).width();
                ww = (ww < 320) ? 320 : ww;
                if (ww <= 800 && search.hasClass("show")) {
                    search.css("width", ww + "px");
                    if (vin.val().length > 0) {
                        clearText.show();
                    }
                }
                else {
                    search.removeAttr("style");
                    clearText.removeAttr("style");
                }
                if (!JPlaceHolder._check()) {
                    if (ww <= 800) {
                        search.find(" > div > span").css({
                            "height": "28px",
                            "line-height": "28px",
                            "padding-left": "38px"
                        });
                    }
                    else {
                        search.find(" > div > span").css({
                            "height": "40px",
                            "line-height": "40px",
                            "padding-left": "10px"
                        });
                    }
                }
            }).trigger("resize");
            // 查询方法
            var searchBtnClick = function () {
                var vinStr = vin.val();
                if (vinStr == "") {
                    vin.focus();
                    if (ww > 800) {
                        alert("请输入Vin码");
                    }
                }
                else {
                    vin.focus();
                    $.ajax({
                        url: "/api/Vehicles/Vin/" + encodeURI(vinStr),
                        type: "get",
                        dataType: "JSON",
                        success: function (datas) {
                            var len = datas.length;
                            if (len == 0) {
                                alert("您输入的VIN码有误！");
                            }
                            else {
                                // 如果有数据则设置Cookie 并渲染查询结果模板
                                $.cookie("VINCookie", vinStr);
                                var responseFirst = datas[0].Models;
                                if (len > 1) {
                                    for (var i = 1; i < len; i++) {
                                        responseFirst = responseFirst.concat(datas[i].Models);
                                    }
                                }
                                var vinDatas = responseFirst;
                                var template = _.template($("#vin_result").html());
                                search_result.html(template({ vinDatas: vinDatas }));
                                // 查询结果的返回Click事件 渲染品牌集合模板
                                $("#back_prev,#c_home").on("click", function () {
                                    $.cookie("VINCookie", { expires: -1 });
                                    renderBrand();
                                });
                            }
                        },
                        error: function () {
                            alert("请输入正确的VIN码");
                        }
                    });
                }
            }
            // 键盘按下事件
            var vinLength = 0;
            $(window).keyup(function (e) {
                vinLength = vin.val().length;
                if (vin.is(":focus")) {
                    if (e && vinLength != 0 && ww <= 800) {
                        clearText.show();
                    }
                    else {
                        clearText.hide();
                        if (window.localStorage) {
                            localStorage.removeItem("VINCode");
                        }
                    }
                }
            }).keydown(function (e) {
                vinLength = vin.val().length;
                if (vin.is(":focus")) {
                    if (e.keyCode == 13) {
                        vinLength = vin.val().length;
                        if (vinLength == 0) {
                            alert("请输入VIN码");
                            return;
                        }
                        searchBtnClick();
                    }
                }
            });
            vin.focus(function () {
                if (vin.val().length != 0 && ww <= 800) {
                    clearText.show();
                }
            }).blur(function () {
                if (window.localStorage && vin.val().length != 0) {
                    var vinstr = vin.val();
                    localStorage.removeItem("VINCode");
                    localStorage.setItem("VINCode", vinstr);
                }
            });
            // 查询绑定click事件
            var isFirst = 0;// 判断在手机浏览器是否第一次点击
            $("#search_btn").on("click", function () {
                if (ww <= 800) {
                    isFirst++;
                    ww = $(window).width();
                    search.addClass("show").css("width", ww + "px");
                    vin.focus();
                    if (isFirst != 1) {
                        searchBtnClick();
                    }
                }
                else {
                    searchBtnClick();
                }
            });

            // 清除搜索框内容的click事件
            clearText.on("click", function () {
                if (window.localStorage) {
                    localStorage.removeItem("VINCode");
                }
                vin.focus().val("");
                vinLength = 0;
                clearText.hide();
            });
        },
        // 品牌车型列表页
        brandModelList: function (id) {
            $.ajax({
                type: "get",
                dataType: "JSON",
                url: "/api/Vehicles/GetManufacturerByBrand/" + encodeURI(id),
                success: function (datas) {
                    var modelList = datas;
                    $(".header .content .title").text("品牌型号");
                    var template = _.template($("#brand_model_list").html());
                    datas_show.html(template({ modelList: modelList }));
                    if (window.localStorage) {
                        var brandModelListHref = window.location.hash;
                        localStorage.removeItem("brandModelListHref");
                        localStorage.setItem("brandModelListHref", brandModelListHref);
                    }
                    $("#back_prev,#c_home").on("click", function () {
                        $.cookie("VINCookie", { expires: -1 });
                        $(this).attr("href", "#");
                    });
                }
            });
        },
        // 车型列表详情
        brandModelListDetails: function (manufacturerCode, modelName) {
            $.ajax({
                type: "get",
                dataType: "JSON",
                url: "/api/Vehicles/Manufacturer/" + encodeURI(manufacturerCode) + "/Model/" + encodeURI(modelName),
                success: function (datas) {
                    var modelDetail = datas.reverse();
                    $(".header .content .title").text(modelDetail[0].Manufacturer + " " + modelDetail[0].ModelName + " " + modelDetail[0].Serie);
                    var template = _.template($("#model_detail").html());
                    datas_show.html(template({ modelDetail: modelDetail }));
                    if (window.localStorage) {
                        var brandModelListDetailsHref = window.location.hash;
                        localStorage.removeItem("brandModelListDetailsHref");
                        localStorage.setItem("brandModelListDetailsHref", brandModelListDetailsHref);
                        var href = localStorage.getItem("brandModelListHref");
                    }
                    $("#back_prev,#c_back").on("click", function () {
                        $(this).attr("href", href);
                    });
                    $("#c_home").on("click", function () {
                        $.cookie("VINCookie", { expires: -1 });
                        $(this).attr("href", "#");
                    });
                }
            });
        },
        // 车子详情
        carDetails: function (id) {
            $.ajax({
                type: "get",
                dataType: "JSON",
                url: "/api/Vehicles/LYId/" + encodeURI(id),
                success: function (datas) {
                    var carDetailsDatas = datas;
                    $(".header .content .title").text("品牌详情");
                    var template = _.template($("#car_details").html());
                    datas_show.html(template({ carDetailsDatas: carDetailsDatas }));
                    if (window.localStorage) {
                        var carDetailsHref = window.location.hash;
                        localStorage.removeItem("carDetailsHref");
                        localStorage.setItem("carDetailsHref", carDetailsHref);
                        var href = localStorage.getItem("brandModelListDetailsHref");
                        var href2 = localStorage.getItem("brandModelListHref");
                    }
                    if ($.cookie("VINCookie") != "[object Object]" && $.cookie("VINCookie") != undefined) {
                        $("#c_back2").hide();
                        $("#c_back2 + span").hide();
                        $("#back_prev,#c_back").on("click", function () {
                            $(this).attr("href", "#");
                        });
                    }
                    else {
                        $("#back_prev,#c_back").on("click", function () {
                            $(this).attr("href", href);
                        });
                    }
                    $("#c_back2").on("click", function () {
                        $(this).attr("href", href2);
                    });
                    $("#c_home").on("click", function () {
                        $.cookie("VINCookie", { expires: -1 });
                        $(this).attr("href", "#");
                    });
                }
            });
        },
        // 保养信息页
        maintainInstruction: function (wvmid) {
            $.ajax({
                type: "get",
                dataType: "JSON",
                url: "/api/Vehicles/MaintenanceItems/" + encodeURI(wvmid),
                success: function (datas) {
                    $.getJSON("/api/Vehicles/LYId/" + encodeURI(datas[0].LyId), function (d) {
                        var title = d.VehicleBrandData.Name + " " + d.ModelName + " " + d.ModelYear + " - " + d.Serie + " " + d.ModelVersion;
                        var maintainDatas = datas;
                        $(".header .content .title").text("保养说明");
                        var template = _.template($("#baoyang_shuoming").html());
                        datas_show.html(template({ maintainDatas: maintainDatas }));
                        $(".caption").find("img").prop("src", d.VehicleBrandData.LogUrl);
                        $(".caption").find("p").text(title);
                        if (window.localStorage) {
                            var maintainInstruction = window.location.hash;
                            localStorage.removeItem("maintainInstruction");
                            localStorage.setItem("maintainInstruction", maintainInstruction);
                            var href2 = localStorage.getItem("brandModelListDetailsHref");
                            var href3 = localStorage.getItem("brandModelListHref");
                            var href = localStorage.getItem("carDetailsHref");
                        }
                        $("#back_prev,#c_back").on("click", function () {
                            $(this).attr("href", href);
                        });
                        if ($.cookie("VINCookie") != "[object Object]" && $.cookie("VINCookie") != undefined) {
                            $("#c_back3").hide();
                            $("#c_back3 + span").hide();
                            $("#c_back2").on("click", function () {
                                $(this).attr("href", "#");
                            });
                        }
                        else {
                            $("#c_back2").on("click", function () {
                                $(this).attr("href", href2);
                            });
                        }
                        $("#c_back3").on("click", function () {
                            $(this).attr("href", href3);
                        });
                        $("#c_home").on("click", function () {
                            $.cookie("VINCookie", { expires: -1 });
                            $(this).attr("href", "#");
                        });
                    });
                }
            });
        },
        // 保养产品页
        productsInformation: function (lyid, mCode) {
            $.ajax({
                type: "get",
                dataType: "JSON",
                url: "/api/Vehicles/" + encodeURI(lyid) + "/" + encodeURI(mCode) + "/Products",
                success: function (datas) {
                    var productDatas = datas.ProductDatas;
                    $(".header .content .title").text("保养产品");
                    var template = _.template($("#product_tp").html());
                    datas_show.html(template({ productDatas: productDatas }));
                    if (window.localStorage) {
                        var productsInformationHref = window.location.hash;
                        localStorage.removeItem("productsInformationHref");
                        localStorage.setItem("productsInformationHref", productsInformationHref);
                        var href2 = localStorage.getItem("carDetailsHref");
                        var href3 = localStorage.getItem("brandModelListDetailsHref");
                        var href4 = localStorage.getItem("brandModelListHref");
                        var href = localStorage.getItem("maintainInstruction");
                    }
                    $("#back_prev,#c_back").on("click", function () {
                        $(this).attr("href", href);
                    });
                    if ($.cookie("VINCookie") != "[object Object]" && $.cookie("VINCookie") != undefined) {
                        $("#c_back4").hide();
                        $("#c_back4 + span").hide();
                        $("#c_back3").on("click", function () {
                            $(this).attr("href", "#");
                        });
                    }
                    else {
                        $("#c_back3").on("click", function () {
                            $(this).attr("href", href3);
                        });
                    }
                    $("#c_back4").on("click", function () {
                        $(this).attr("href", href4);
                    });
                    $("#c_back2").on("click", function () {
                        $(this).attr("href", href2);
                    });
                    $("#c_home").on("click", function () {
                        $.cookie("VINCookie", { expires: -1 });
                        $(this).attr("href", "#");
                    });
                    $(".goods .container:last").css("border", "none");
                }
            });
        }
    });
    var router = new Router();
    Backbone.history.start();
});