<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:xhtml="http://www.w3.org/1999/xhtml" version="1.0">

  <xsl:template match="/">
    <html>
      <head>
        <META http-equiv="Content-Type" content="text/html; charset=GB2312"></META>
        <style>
          table.claims {

          width:100%;
          align:center;
          border-collapse: collapse;
          table-layout:fixed;

          }

          td.title{
          background-color:#C8D6EE;
          text-align:center;
          font-family:;
          font-size:20;
          font-weight: bold;

          }

          #trans{
          text-align:right;
          }

          a{
          background-color:#CCFFFF;
          }
        </style>
        <script type="text/javascript">

        </script>
      </head>
      <body>
        <div id="divDes">
          <xsl:apply-templates select="cn-patent-document"/>
        </div>
      </body>
      <!--../Translation/QuickTrans.aspx?AppNo=198510000001&Doc=D-->
    </html>
  </xsl:template>

  <xsl:template match="cn-patent-document">
    <xsl:apply-templates select="description"/>
  </xsl:template>

  <xsl:template match="description">
    <table class="description"  width="100%" align="center">
      <tr>
        <td>
          <!--<xsl:apply-templates/>-->
          <xsl:value-of select="." disable-output-escaping="yes"/>
        </td>
      </tr>
    </table>
    <br/>
  </xsl:template>

  <xsl:template match="invention-title">
    <div class="title">
      <xsl:value-of select="."/>
    </div>
  </xsl:template>

  <xsl:template match="p">
    <p>
      　<xsl:apply-templates/>
    </p>
  </xsl:template>

  <xsl:template match="comment()">
    <xsl:choose>
      <xsl:when test="contains(.,'SIPO')">
        <div align="center">
          <br/>
          --&#160;
          <script language="JavaScript" type="text/javascript">
            var pageinfo = '<xsl:value-of select="substring(.,14)"/>';
            var end = pageinfo.indexOf('"');
            var num = pageinfo.substring(0,end);
            document.write(num);
          </script>
          &#160;--
        </div>
        <br/>
      </xsl:when>
      <xsl:when test="contains(.,'no marking')">
        &lt;标记未做五项标引&gt;
      </xsl:when>
      <xsl:when test="contains(.,'part marking')">
        &lt;部分五项标引&gt;
      </xsl:when>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="sub">
    <sub>
      <xsl:apply-templates select="* |text()"/>
    </sub>
  </xsl:template>

  <xsl:template match="sup">
    <sup>
      <xsl:apply-templates select="* |text()"/>
    </sup>
  </xsl:template>

  <xsl:template match="tables">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="table">
    <div align="center">
      <table size="4">
        <xsl:attribute name="align">
          <xsl:value-of select="@align"/>
        </xsl:attribute>
        <xsl:attribute name="width">
          <xsl:value-of select="@pgwide"/>
        </xsl:attribute>
        <xsl:attribute name="cellspacing">
          <xsl:value-of select="@cellspacing"/>
        </xsl:attribute>
        <xsl:attribute name="border">
          <xsl:value-of select="@border"/>
        </xsl:attribute>
        <xsl:attribute name="frame">
          <xsl:value-of select="@frame"/>
        </xsl:attribute>
        <xsl:apply-templates/>
      </table>
    </div>
  </xsl:template>
  <xsl:template match="tgroup">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="colspec">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="thead">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="tbody">
    <xsl:apply-templates/>
  </xsl:template>
  <xsl:template match="row">
    <tr>
      <xsl:attribute name="valign">
        <xsl:value-of select="@valign"/>
      </xsl:attribute>
      <xsl:attribute name="align">
        <xsl:value-of select="@align"/>
      </xsl:attribute>
      <xsl:apply-templates/>
    </tr>
  </xsl:template>
  <xsl:template match="entry">
    <script language="JavaScript" type="text/javascript">
      var morerows = <xsl:value-of select="@morerows"/>;
      var namest = "<xsl:value-of select="@namest"/>";
      var nameend = "<xsl:value-of select="@nameend"/>";
      var rowspan = "";
      var colspan = "";
      if(morerows > 1)
      {
      rowspan = "rowspan="+morerows;
      }
      if(namest != null &amp;&amp; nameend != null)
      {
      var start = namest.substring(1);
      var end = nameend.substring(1);
      var gap = end - start + 1;
      colspan = "colspan="+gap;
      }
      var td = "&lt;td" + " " + rowspan + " " + colspan + "&gt;";
      document.write(td);
    </script>
    <xsl:attribute name="valign">
      <xsl:value-of select="@valign"/>
    </xsl:attribute>
    <xsl:attribute name="align">
      <xsl:value-of select="@align"/>
    </xsl:attribute>
    <xsl:apply-templates/>
    <script language="JavaScript" type="text/javascript">
      <xsl:if test="not(text())">
        document.write("&amp;nbsp;");
      </xsl:if>
      document.write("&lt;/td&gt;");
    </script>
  </xsl:template>
</xsl:stylesheet>

