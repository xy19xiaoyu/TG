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
        </style>
				<script type="text/javascript">
					
				</script>
			</head>
			<body>
				<xsl:apply-templates select="cn-patent-document"/>
			</body>
		</html>
	</xsl:template>
	
	<xsl:template match="cn-patent-document">
		<xsl:apply-templates select="claims"/>
	</xsl:template>
	
	<xsl:template match="claims">
		<table class="claims"  width="100%" align="center">
			<tr>
				<td>					
					<xsl:apply-templates select="claim"/>
				</td>
			</tr>
		</table>
		<br/>
	</xsl:template>
	
	<xsl:template match="claim">
		<p>
		　<xsl:apply-templates select="claim-text"/>
		</p>
	</xsl:template>
	
	<xsl:template match="claim-text">
		<!--<xsl:apply-templates/>-->
    <xsl:value-of select="." disable-output-escaping="yes"/>
	</xsl:template>
	
	
	<xsl:template match="comment()">
	<xsl:choose>
	  	<xsl:when test="contains(.,'SIPO')">
			<div align="center">
			<br/>
			&#160;
			<script language="JavaScript" type="text/javascript">
				var pageinfo = '<xsl:value-of select="substring(.,14)"/>';
				var end = pageinfo.indexOf('"');
				var num = pageinfo.substring(0,end);
				document.write(num);
			</script>
			&#160;
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
	
	
	
</xsl:stylesheet>