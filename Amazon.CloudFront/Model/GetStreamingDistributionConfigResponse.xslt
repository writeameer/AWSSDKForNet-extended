﻿<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
        xmlns:cf="http://cloudfront.amazonaws.com/doc/2009-12-01/" exclude-result-prefixes="xsl cf">
	<xsl:output method="xml" omit-xml-declaration="no" indent="yes"/>
	<xsl:variable name="ns" select="'http://cloudfront.amazonaws.com/doc/2009-12-01/'"/>

	<xsl:template match="cf:StreamingDistributionConfig">
		<xsl:element name="GetStreamingDistributionConfigResponse" namespace="{$ns}">
			<xsl:element name="StreamingDistributionConfig" namespace="{$ns}">
				<xsl:apply-templates/>
			</xsl:element>
		</xsl:element>
	</xsl:template>

	<xsl:template match="*">
		<xsl:copy>
			<xsl:copy-of select="@*" />
			<xsl:apply-templates />
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>