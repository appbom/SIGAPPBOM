/*
SQLyog Ultimate v8.3 
MySQL - 5.5.22 : Database - sigappbom
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`sigappbom` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_spanish_ci */;

USE `sigappbom`;

/*Table structure for table `articulo` */

DROP TABLE IF EXISTS `articulo`;

CREATE TABLE `articulo` (
  `Id` int(10) NOT NULL COMMENT 'Id de Articulo',
  `CodigoCatalogo` varchar(8) COLLATE utf8_spanish_ci NOT NULL COMMENT 'Codigo de Catalogación',
  `Nombre` varchar(100) COLLATE utf8_spanish_ci NOT NULL COMMENT 'Nombre de articulo',
  `Stock` int(11) NOT NULL COMMENT 'Stock de articulo en el almacen',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `articulo` */

insert  into `articulo`(`Id`,`CodigoCatalogo`,`Nombre`,`Stock`) values (101,'COD001','Caja 13 Kilos',100),(102,'COD001','Caja 13 Kilos',100);

/*Table structure for table `claves` */

DROP TABLE IF EXISTS `claves`;

CREATE TABLE `claves` (
  `Tabla` varchar(20) COLLATE utf8_spanish_ci DEFAULT NULL,
  `NextHi` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `claves` */

insert  into `claves`(`Tabla`,`NextHi`) values ('articulo',2),('pedido',2),('detallepedido',2);

/*Table structure for table `detallepedido` */

DROP TABLE IF EXISTS `detallepedido`;

CREATE TABLE `detallepedido` (
  `Id` int(10) NOT NULL COMMENT 'Id de detalle de pedido',
  `PedidoId` int(10) NOT NULL COMMENT 'Id de pedido',
  `ArticuloId` int(10) NOT NULL COMMENT 'Id de artículo',
  `CantidadSolicitada` int(11) NOT NULL,
  `CantidadAtendida` int(11) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_DetallePedido_Articulo` (`ArticuloId`),
  KEY `FK_DetallePedido_Pedido` (`PedidoId`),
  CONSTRAINT `FK_DetallePedido_Articulo` FOREIGN KEY (`ArticuloId`) REFERENCES `articulo` (`Id`),
  CONSTRAINT `FK_DetallePedido_Pedido` FOREIGN KEY (`PedidoId`) REFERENCES `pedido` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `detallepedido` */

insert  into `detallepedido`(`Id`,`PedidoId`,`ArticuloId`,`CantidadSolicitada`,`CantidadAtendida`) values (101,101,101,10,0),(102,101,102,10,0);

/*Table structure for table `pedido` */

DROP TABLE IF EXISTS `pedido`;

CREATE TABLE `pedido` (
  `Id` int(10) NOT NULL COMMENT 'Id de pedido',
  `Descripcion` varchar(200) COLLATE utf8_spanish_ci NOT NULL COMMENT 'Descripción de pedido',
  `Solicitante` varchar(100) COLLATE utf8_spanish_ci NOT NULL COMMENT 'Persona que hizo el pedido',
  `Estado` int(1) NOT NULL COMMENT 'Estado del pedido',
  `FechaCreacion` date NOT NULL COMMENT 'Fecha de Creación',
  `FechaAtencion` date DEFAULT NULL COMMENT 'Fecha de Atención',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `pedido` */

insert  into `pedido`(`Id`,`Descripcion`,`Solicitante`,`Estado`,`FechaCreacion`,`FechaAtencion`) values (101,'Test Integracion 1','Test',1,'2012-06-14',NULL),(102,'Test Integracion 2','Test',2,'2012-06-14',NULL);

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
