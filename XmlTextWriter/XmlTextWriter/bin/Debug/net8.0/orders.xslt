<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="html" indent="yes" />

	<xsl:template match="/">
		<html>
			<head>
				<title>Информация о заказах</title>
				<style>
					table {
					width: 100%;
					border-collapse: collapse;
					}
					th, td {
					border: 1px solid black;
					padding: 8px;
					text-align: left;
					}
					th {
					background-color: #f2f2f2;
					}
				</style>
			</head>
			<body>
				<h1>Список заказов</h1>
				<xsl:for-each select="Orders/Order">
					<h2>
						Заказ № <xsl:value-of select="@OrderId" />
					</h2>
					<p>
						<strong>Имя клиента:</strong>
						<xsl:value-of select="CustomerName" />
					</p>
					<p>
						<strong>Дата заказа:</strong>
						<xsl:value-of select="OrderDate" />
					</p>

					<table>
						<thead>
							<tr>
								<th>Название товара</th>
								<th>Количество</th>
								<th>Цена</th>
							</tr>
						</thead>
						<tbody>
							<xsl:for-each select="Items/Item">
								<tr>
									<td>
										<xsl:value-of select="Name" />
									</td>
									<td>
										<xsl:value-of select="Quantity" />
									</td>
									<td>
										<xsl:value-of select="Price" />
									</td>
								</tr>
							</xsl:for-each>
						</tbody>
					</table>
				</xsl:for-each>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>