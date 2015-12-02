<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmDesignImgs.aspx.cs"
    Inherits="Patentquery.My.frmDesignImgs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../Js/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        var nSizi = 500;

        $(document).ready(function () {
            //点击小图切换大图
            $("#thumbnail li a").click(function () {
                $("#divZoompic").css({ background: 'url(../images/loding_imgFt.gif) no-repeat 50% 50%' });

                $(".zoompic img").hide().attr({ "src": $(this).attr("href") + '&bg=', "title": $("> img", this).attr("title") });

                $("#thumbnail li.current").removeClass("current");
                $(this).parents("li").addClass("current");
                return false;
            });
            $(".zoompic>img").load(function () {
                var imgTmp = new Image();
                imgTmp.onload = function () {
                    var imgObj = this;
                    if (imgObj.height > imgObj.width && imgObj.height > nSizi) {
                        imgObj.width = imgObj.width / (imgObj.height / nSizi);
                        imgObj.height = nSizi;
                    } else if (imgObj.width > nSizi) {
                        imgObj.height = imgObj.height / (imgObj.width / nSizi);
                        imgObj.width = nSizi;
                    }
                    $(".zoompic>img:hidden").attr({ "height": imgObj.height, "width": imgObj.width });
                    $("#divZoompic").css({ background: '' });
                    $(".zoompic>img:hidden").show();
                }
                imgTmp.src = this.src;
            });

            //小图片左右滚动
            var $slider = $('.slider ul');
            var $slider_child_l = $('.slider ul li').length;
            var $slider_width = $('.slider ul li').width();
            $slider.width($slider_child_l * $slider_width);

            var slider_count = 0;

            if ($slider_child_l < 5) {
                $('#btn-right').css({ cursor: 'auto' });
                $('#btn-right').removeClass("dasabled");
            }

            $('#btn-right').click(function () {
                if ($slider_child_l < 5 || slider_count >= $slider_child_l - 5) {
                    return false;
                }

                slider_count++;
                $slider.animate({ left: '-=' + $slider_width + 'px' }, 'fast');
                slider_pic();
            });

            $('#btn-left').click(function () {
                if (slider_count <= 0) {
                    return false;
                }
                slider_count--;
                $slider.animate({ left: '+=' + $slider_width + 'px' }, 'fast');
                slider_pic();
            });

            function slider_pic() {
                if (slider_count >= $slider_child_l - 5) {
                    $('#btn-right').css({ cursor: 'auto' });
                    $('#btn-right').addClass("dasabled");
                }
                else if (slider_count > 0 && slider_count <= $slider_child_l - 5) {
                    $('#btn-left').css({ cursor: 'pointer' });
                    $('#btn-left').removeClass("dasabled");
                    $('#btn-right').css({ cursor: 'pointer' });
                    $('#btn-right').removeClass("dasabled");
                }
                else if (slider_count <= 0) {
                    $('#btn-left').css({ cursor: 'auto' });
                    $('#btn-left').addClass("dasabled");
                }
            }

            try {
                $("#thumbnail li a")[0].click();
            } catch (e) {
            }

        });

        function imgOnError(obj) {
            obj.onerror = "";
            obj.src = "../Images/NoImg_300.jpg";
        }
    </script>
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
            list-style-type: none;
        }
        a, img
        {
            border: 0;
        }
        body
        {
            font: 12px/180% Arial, Helvetica, sans-serif, "宋体";
        }
        /* zoombox */
        .zoombox
        {
            width: 705px;
            margin: 20px auto 0 auto;
        }
        .zoompic
        {
            border: solid 1px #dfdfdf;
            width: 700px;
            height: 500px;
            background: url(../images/loding_imgFt.gif) no-repeat 50% 50%;
            line-height: 500px;
            top: -50%;
            display: table-cell;
        }
        .zoompic_bgimg
        {
            background: url(../images/loding_imgFt.gif) no-repeat 50% 50%;
        }
        
        .zoompic_bgimgRemove
        {
            background: url(../images/arrow-btn.png) no-repeat 50% 50%;
        }
        
        .sliderbox
        {
            height: 76px;
            overflow: hidden;
            margin: 6px 0 0 0;
        }
        .sliderbox .arrow-btn
        {
            width: 38px;
            height: 76px;
            background: url(../images/arrow-btn.png) no-repeat;
            cursor: pointer;
        }
        .sliderbox #btn-left
        {
            float: left;
            background-position: 0 0;
        }
        .sliderbox #btn-left.dasabled
        {
            background-position: 0 -76px;
        }
        .sliderbox #btn-right
        {
            float: right;
            background-position: -38px 0;
        }
        .sliderbox #btn-right.dasabled
        {
            background-position: -38px -76px;
        }
        .sliderbox .slider
        {
            float: left;
            height: 76px;
            width: 625px;
            position: relative;
            overflow: hidden;
            margin: 0 0 0 3px;
            display: inline;
        }
        .sliderbox .slider ul
        {
            position: absolute;
            left: 0;
            width: 999em;
        }
        .sliderbox .slider li
        {
            float: left;
            width: 121px;
            height: 76px;
            text-align: center;
        }
        .sliderbox .slider li img
        {
            border: solid 1px #dfdfdf;
            width: 115px;
            height: 74px;
        }
        .sliderbox .slider li.current img
        {
            border: solid 1px #3366cc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divZoomBox" class="zoombox" runat="server">
        <div id="divZoompic" class="zoompic" style="vertical-align: middle; text-align: center;">
            <asp:Image ID="ImagePicture" ClientIDMode="Static" runat="server" ImageUrl="~/Images/loding_imgFt.gif" />
        </div>
        <div class="sliderbox">
            <div id="btn-left" class="arrow-btn dasabled">
            </div>
            <div class="slider" id="thumbnail">
                <ul>
                    <%--<li class="current"><a href="images/3427.jpg" target="_blank">
                        <img src="images/14fd.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸01" /></a></li>
                    <li><a href="images/52347.jpg" target="_blank">
                        <img src="images/41a.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸02" /></a></li>
                    <li><a href="images/23463.jpg" target="_blank">
                        <img src="images/234fa.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸03" /></a></li>
                    <li><a href="images/3247.jpg" target="_blank">
                        <img src="images/412saf.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸04" /></a></li>
                    <li><a href="images/26547.jpg" target="_blank">
                        <img src="images/41356a.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸05" /></a></li>
                    <li><a href="images/2153.jpg" target="_blank">
                        <img src="images/432sadf.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸06" /></a></li>
                    <li><a href="images/3427.jpg" target="_blank">
                        <img src="images/14fd.jpg" width="115" height="74" alt="美女配奥迪A4L墙纸01" /></a></li>--%>
                    <asp:Literal ID="litDesigImgsLi" runat="server" />
                </ul>
            </div>
            <div id="btn-right" class="arrow-btn">
            </div>
        </div>
    </div>
    <div id="divNoImg" style="width: 100%; text-align: center; vertical-align: middle;"
        runat="server">
        <table style="width: 100%;">
            <tr>
                <td style="width: 50%; text-align: right;">
                    <img src="/Images/iconImportant.png" alt="" />
                </td>
                <td style="width: 50%; text-align: left; font-size: 18;">
                    <strong>没有可供展示的图片数据 </strong>
                </td>
            </tr>
        </table>
    </div>
    <!--slider end-->
    </form>
</body>
</html>
