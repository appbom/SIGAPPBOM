/*
SQLyog Ultimate v8.3 
MySQL - 5.5.22 : Database - sigappbom_test
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`sigappbom_test` /*!40100 DEFAULT CHARACTER SET utf8 COLLATE utf8_spanish_ci */;

USE `sigappbom_test`;

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

/*Table structure for table `claves` */

DROP TABLE IF EXISTS `claves`;

CREATE TABLE `claves` (
  `Tabla` varchar(20) COLLATE utf8_spanish_ci DEFAULT NULL,
  `NextHi` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `claves` */

insert  into `claves`(`Tabla`,`NextHi`) values ('articulo',5),('pedido',4),('detallepedido',4),('notasalida',2),('detallenotasalida',2);

/*Table structure for table `detallenotasalida` */

DROP TABLE IF EXISTS `detallenotasalida`;

CREATE TABLE `detallenotasalida` (
  `id` int(10) NOT NULL,
  `notasalidaid` int(10) NOT NULL,
  `articuloid` int(10) NOT NULL,
  `cantidad` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_detallenotasalida_notasalida` (`notasalidaid`),
  KEY `FK_detallenotasalida_articulo` (`articuloid`),
  CONSTRAINT `FK_detallenotasalida_articulo` FOREIGN KEY (`articuloid`) REFERENCES `articulo` (`Id`),
  CONSTRAINT `FK_detallenotasalida_notasalida` FOREIGN KEY (`notasalidaid`) REFERENCES `notasalida` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `detallenotasalida` */

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

/*Table structure for table `notasalida` */

DROP TABLE IF EXISTS `notasalida`;

CREATE TABLE `notasalida` (
  `id` int(10) NOT NULL,
  `fecha` date NOT NULL,
  `observacion` varchar(300) COLLATE utf8_spanish_ci DEFAULT NULL,
  `pedidoid` int(10) NOT NULL,
  `usuario` varchar(50) COLLATE utf8_spanish_ci NOT NULL,
  PRIMARY KEY (`id`),
  KEY `FK_notasalida_pedido` (`pedidoid`),
  CONSTRAINT `FK_notasalida_pedido` FOREIGN KEY (`pedidoid`) REFERENCES `pedido` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `notasalida` */

/*Table structure for table `pedido` */

DROP TABLE IF EXISTS `pedido`;

CREATE TABLE `pedido` (
  `Id` int(10) NOT NULL COMMENT 'Id de pedido',
  `Descripcion` varchar(200) COLLATE utf8_spanish_ci NOT NULL COMMENT 'Descripción de pedido',
  `Solicitante` varchar(100) COLLATE utf8_spanish_ci NOT NULL COMMENT 'Persona que solicita el pedido',
  `Estado` int(1) NOT NULL COMMENT 'Estado del pedido',
  `FechaCreacion` date NOT NULL COMMENT 'Fecha de Creación',
  `FechaAtencion` date DEFAULT NULL COMMENT 'Fecha de Atención',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_spanish_ci;

/*Data for the table `pedido` */

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
