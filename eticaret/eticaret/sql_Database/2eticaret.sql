-- --------------------------------------------------------
-- Sunucu:                       127.0.0.1
-- Sunucu sürümü:                8.0.38 - MySQL Community Server - GPL
-- Sunucu İşletim Sistemi:       Win64
-- HeidiSQL Sürüm:               12.7.0.6850
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- eticarets için veritabanı yapısı dökülüyor
CREATE DATABASE IF NOT EXISTS `eticarets` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `eticarets`;

-- tablo yapısı dökülüyor eticarets.categories
CREATE TABLE IF NOT EXISTS `categories` (
  `CategoryID` int NOT NULL AUTO_INCREMENT,
  `CategoryName` varchar(255) NOT NULL,
  PRIMARY KEY (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.categories: ~4 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `categories` (`CategoryID`, `CategoryName`) VALUES
	(1, 'Fashion'),
	(2, 'Electronics'),
	(3, 'Sporting Goods'),
	(4, 'Home & Garden');

-- tablo yapısı dökülüyor eticarets.deletedrecords
CREATE TABLE IF NOT EXISTS `deletedrecords` (
  `id` int NOT NULL AUTO_INCREMENT,
  `TableName` varchar(255) DEFAULT NULL,
  `DeletedData` json DEFAULT NULL,
  `DeletedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.deletedrecords: ~0 rows (yaklaşık) tablosu için veriler indiriliyor

-- tablo yapısı dökülüyor eticarets.deleted_users
CREATE TABLE IF NOT EXISTS `deleted_users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Kullanici_adi` varchar(50) DEFAULT NULL,
  `Sifre` varchar(100) DEFAULT NULL,
  `EMail` varchar(100) DEFAULT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `Role` bit(1) DEFAULT NULL,
  `Cinsiyet` bit(1) DEFAULT NULL,
  `D_Tarihi` date DEFAULT NULL,
  `Giris_Sayisi` int DEFAULT NULL,
  `Giris_Tarihi` date DEFAULT NULL,
  `Giris_Yapabilirmi` bit(1) DEFAULT NULL,
  `DeletedAt` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.deleted_users: ~8 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `deleted_users` (`id`, `Kullanici_adi`, `Sifre`, `EMail`, `PhoneNumber`, `Role`, `Cinsiyet`, `D_Tarihi`, `Giris_Sayisi`, `Giris_Tarihi`, `Giris_Yapabilirmi`, `DeletedAt`) VALUES
	(1, 'edef', '123', 'semih2201dasdsasaad73@ogr.duzce.edu.tr', 'ewf', b'0', b'1', '2001-10-11', 3, NULL, b'1', '2024-08-16 19:44:40'),
	(2, 'dasasd', '123', 'dasadsdasda@das', 'dasdasdsa', b'0', b'1', '2005-05-12', 3, NULL, b'1', '2024-08-16 19:44:40'),
	(3, 'semih', '123', 'jhngbnhb@nhnmrlg', '78465123', b'1', b'1', '2002-10-19', 3, NULL, b'1', '2024-08-16 19:45:12'),
	(4, 'asd', 'asddsa', 'semih220dasdasdsaads173@ogr.duzce.edu.tr', 'dsa', b'0', b'1', '2024-08-15', 3, NULL, b'1', '2024-08-16 19:45:12'),
	(5, 'semih9 ', 'asddsa', 'wdsaasd@gmail.com', '87465312', b'0', b'1', '2002-10-19', 3, NULL, b'1', '2024-08-16 19:45:12'),
	(6, 'eqwdsa', '123321', 'semihassdasd220173@ogr.duzce.edu.tr', 'wqdsa', b'0', b'1', '2005-10-19', 3, NULL, b'1', '2024-08-16 19:45:12'),
	(7, '312', '321', '123@asdda', '312', b'0', b'1', '2002-11-11', 3, NULL, b'1', '2024-08-16 19:45:12'),
	(8, 'rgthyujı', 'asdasdasd', 'semih220173@ogr.duzce.edu.tr', 'lokuyjhtgrf', b'0', b'1', '2000-02-12', 3, NULL, b'1', '2024-08-16 19:45:12');

-- olay yapısı dökülüyor eticarets.delete_giris_yapildi
DELIMITER //
CREATE EVENT `delete_giris_yapildi` ON SCHEDULE EVERY 5 HOUR STARTS '2024-08-16 22:29:23' ON COMPLETION NOT PRESERVE ENABLE DO DELETE FROM giris_yapildi//
DELIMITER ;

-- tablo yapısı dökülüyor eticarets.giris_yapildi
CREATE TABLE IF NOT EXISTS `giris_yapildi` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Kullanici_adi` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0',
  `EMail` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT '0',
  `Profil_Foto` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=446 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.giris_yapildi: ~0 rows (yaklaşık) tablosu için veriler indiriliyor

-- tablo yapısı dökülüyor eticarets.kullanicininsattiklari
CREATE TABLE IF NOT EXISTS `kullanicininsattiklari` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UrunId` int NOT NULL,
  `UrunAdi` varchar(255) NOT NULL,
  `UrunAciklama` text,
  `UrunFiyat` decimal(10,2) NOT NULL,
  `UrunResim` varchar(255) DEFAULT NULL,
  `SaticiId` int NOT NULL,
  `SatinAlanId` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `SaticiId` (`SaticiId`),
  KEY `SatinAlanId` (`SatinAlanId`),
  CONSTRAINT `kullanicininsattiklari_ibfk_1` FOREIGN KEY (`SaticiId`) REFERENCES `users` (`id`),
  CONSTRAINT `kullanicininsattiklari_ibfk_2` FOREIGN KEY (`SatinAlanId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.kullanicininsattiklari: ~8 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `kullanicininsattiklari` (`Id`, `UrunId`, `UrunAdi`, `UrunAciklama`, `UrunFiyat`, `UrunResim`, `SaticiId`, `SatinAlanId`) VALUES
	(2, 6, 'Parfüm', 'wfdcvvdw', 2130.00, '3e67ac78-e666-4f24-9b23-966630032692.jpeg', 16, 10),
	(3, 22, 'Kamera', 'yeni kamera', 12000.00, 'urun-cekimi.png', 10, 19),
	(4, 21, 'telefon ', 'yeni telefon', 12000.00, 'huawei.png', 10, 19),
	(5, 7, 'Güzellik Seti', 'Güzellik Seti', 999.00, '1d5584aa-6385-4543-9b01-0ad80ffc0c9a.png', 16, 20),
	(6, 25, 'yeni kamera', 'asasddas asdasdas', 12345.00, 'urun-cekimi.png', 21, 20),
	(7, 24, 'kamera ', 'dasdasads dasdasds', 12000.00, 'urun-cekimi.png', 20, 22),
	(8, 26, 'asteroid destroyer', 'bla bla bla bla ', 18000000.00, 'Ekran görüntüsü 2024-08-05 221622.png', 22, 23),
	(9, 27, 'baran', 'dsads dsadasd dasasdds', 1987.00, 'Ekran görüntüsü 2024-07-22 140717.png', 23, 10);

-- tablo yapısı dökülüyor eticarets.products
CREATE TABLE IF NOT EXISTS `products` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `price` decimal(10,2) DEFAULT NULL,
  `description` text,
  `img` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `label` varchar(50) DEFAULT NULL,
  `discount` int DEFAULT NULL,
  `category_id` int DEFAULT NULL,
  `cinsiyet` bit(1) DEFAULT NULL,
  `cocuk` bit(1) DEFAULT NULL,
  `alan_adi` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `category_id` (`category_id`),
  CONSTRAINT `products_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`CategoryID`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.products: ~18 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `products` (`id`, `name`, `price`, `description`, `img`, `label`, `discount`, `category_id`, `cinsiyet`, `cocuk`, `alan_adi`) VALUES
	(1, 'Samsung Galaxy S21', 899.99, NULL, 'samsung.png', NULL, NULL, 2, NULL, NULL, NULL),
	(2, 'Coffee machine', 139.00, NULL, 'product-7.png', NULL, NULL, 4, NULL, NULL, NULL),
	(3, 'Iron set', 1499.00, NULL, 'product-8.png', NULL, NULL, 4, NULL, NULL, NULL),
	(4, 'Projector 4k', 1499.99, NULL, 'Ekran görüntüsü 2024-09-10 175216.png', NULL, NULL, 2, NULL, NULL, NULL),
	(5, 'Vacuum cleaner', 1499.00, NULL, 'product-11.png', NULL, NULL, 4, NULL, NULL, 'süpürgee'),
	(6, 'E-shaver 500', 1499.00, NULL, 'product-12.png', NULL, NULL, 2, NULL, NULL, NULL),
	(7, 'Polo shirt', 129.00, NULL, 'cloth-2.jpg', NULL, NULL, 1, NULL, NULL, NULL),
	(8, 'T-shirt', 29.00, NULL, 'cloth-3.jpg', NULL, NULL, 1, NULL, NULL, NULL),
	(9, 'Winter Jacket', 299.00, NULL, 'cloth-4.jpg', NULL, NULL, 1, NULL, NULL, NULL),
	(10, 'Sweater', 19.00, NULL, 'cloth-5.jpg', NULL, NULL, 1, NULL, NULL, NULL),
	(11, 'Lether jacket', 499.00, NULL, 'cloth-6.jpg', NULL, NULL, 1, NULL, NULL, NULL),
	(12, 'Man Shoe', 139.00, NULL, 'product-1.png', NULL, NULL, 1, NULL, NULL, NULL),
	(13, 'Sport watch', 245.00, NULL, 'product-2.png', NULL, NULL, 4, b'1', NULL, NULL),
	(14, '4k TV', 2699.00, NULL, 'product-3.png', NULL, NULL, 4, NULL, NULL, NULL),
	(15, 'Drone machine', 399.00, NULL, 'product-4.png', NULL, NULL, 2, NULL, NULL, NULL),
	(18, 'Redmi2 ', 12354.00, 'jkhmgtr', 'huawei.png', 'yeni', 12, 1, b'0', b'1', 'Telefon'),
	(22, 'Danbıl', 2000.00, 'dasadsdas dasadsds', 'Ekran görüntüsü 2024-09-12 224041.png', 'yeni', 12, 3, NULL, NULL, 'spor');

-- olay yapısı dökülüyor eticarets.ResetGirisDurumu
DELIMITER //
CREATE EVENT `ResetGirisDurumu` ON SCHEDULE EVERY 1 DAY STARTS '2024-08-15 23:39:06' ON COMPLETION NOT PRESERVE ENABLE DO UPDATE Users
  SET Giris_Tarihi = 3, Giris_Yapabilirmi = 1
  WHERE Giris_Tarihi < CURDATE()//
DELIMITER ;

-- tablo yapısı dökülüyor eticarets.satin_alinanlar
CREATE TABLE IF NOT EXISTS `satin_alinanlar` (
  `id` int NOT NULL AUTO_INCREMENT,
  `ProductId` int DEFAULT NULL,
  `UserId` int DEFAULT NULL,
  `PurchaseDate` datetime DEFAULT NULL,
  PRIMARY KEY (`id`) USING BTREE,
  KEY `ProductId` (`ProductId`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `satin_alinanlar_ibfk_1` FOREIGN KEY (`ProductId`) REFERENCES `products` (`id`),
  CONSTRAINT `satin_alinanlar_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.satin_alinanlar: ~4 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `satin_alinanlar` (`id`, `ProductId`, `UserId`, `PurchaseDate`) VALUES
	(1, 4, 16, '2024-08-19 16:06:20'),
	(2, 14, 16, '2024-08-19 21:26:19'),
	(3, 4, 16, '2024-08-20 11:27:10'),
	(4, 6, 16, '2024-08-20 11:29:44'),
	(5, 4, 16, '2024-08-20 11:31:17');

-- tablo yapısı dökülüyor eticarets.satisakoyulanurunsayisi
CREATE TABLE IF NOT EXISTS `satisakoyulanurunsayisi` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserID` int DEFAULT NULL,
  `ToplamSatisaKoyulanUrunSayisi` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `UserID` (`UserID`),
  CONSTRAINT `satisakoyulanurunsayisi_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.satisakoyulanurunsayisi: ~3 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `satisakoyulanurunsayisi` (`id`, `UserID`, `ToplamSatisaKoyulanUrunSayisi`) VALUES
	(2, 16, 1),
	(4, 19, 1),
	(9, 23, 1);

-- tablo yapısı dökülüyor eticarets.sellers
CREATE TABLE IF NOT EXISTS `sellers` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `urunadi` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `urunaciklama` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `urunresim` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `urunkategori` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Stok` int DEFAULT NULL,
  `Sehir` char(50) DEFAULT NULL,
  `ilce` char(50) DEFAULT NULL,
  `urunfiyat` int DEFAULT NULL,
  `urundurumu` char(50) DEFAULT NULL,
  `marka` char(50) DEFAULT NULL,
  `Onay_Asaması` enum('Onaylandı','Onaylanmadı','Bekleniyor','Askıya Alındı','Satıldı') CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT 'Bekleniyor',
  `SatisaKoyulanUrun` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `sellers_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.sellers: ~3 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `sellers` (`Id`, `UserId`, `urunadi`, `urunaciklama`, `urunresim`, `urunkategori`, `Stok`, `Sehir`, `ilce`, `urunfiyat`, `urundurumu`, `marka`, `Onay_Asaması`, `SatisaKoyulanUrun`) VALUES
	(5, 16, 'Güneş Gözlüğü', 'bgrgbvdfk gntkefm  ntkegmfl nkrmşlef ', '337a9741-a220-4c75-88da-62afca06ec4e.jpg', 'gözlük', NULL, 'Ankara', 'Kadıköy', 1241, 'Yenilenmiş', 'SemihYürükcü', 'Onaylandı', NULL),
	(23, 19, 'Parfüm', 'dasads asdasdads', 'images.jpeg', 'Huawei', NULL, 'İstanbul', 'Kadıköy', 999, 'Yenilenmiş', 'SemihYürükcü', 'Onaylandı', NULL),
	(28, 23, 'bisi', 'dasdsdas', 'Ekran görüntüsü 2024-07-16 130543.png', 'Samsung', NULL, 'İstanbul', 'Kadıköy', 12312, 'İkinci El', 'SemihYürükcü', 'Onaylanmadı', NULL);

-- tablo yapısı dökülüyor eticarets.sepete_eklenen_urunler
CREATE TABLE IF NOT EXISTS `sepete_eklenen_urunler` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int DEFAULT NULL,
  `product_id` int DEFAULT NULL,
  `satın_alma_zamani` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `sepete_eklenen_urunler_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
  CONSTRAINT `sepete_eklenen_urunler_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=144 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.sepete_eklenen_urunler: ~15 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `sepete_eklenen_urunler` (`id`, `user_id`, `product_id`, `satın_alma_zamani`) VALUES
	(81, 19, 4, '2024-09-10 17:53:44'),
	(89, 19, 5, '2024-09-11 23:22:05'),
	(90, 19, 5, '2024-09-11 23:23:59'),
	(91, 19, 5, '2024-09-11 23:26:13'),
	(92, 19, 5, '2024-09-11 23:26:56'),
	(93, 19, 5, '2024-09-11 23:27:32'),
	(94, 19, 5, '2024-09-11 23:29:19'),
	(95, 19, 5, '2024-09-11 23:31:29'),
	(96, 19, 5, '2024-09-11 23:36:35'),
	(103, 21, 6, '2024-09-15 10:47:51'),
	(104, 21, 6, '2024-09-15 10:48:42'),
	(105, 20, 2, '2024-09-16 13:31:51'),
	(106, 20, 3, '2024-09-16 13:38:51'),
	(107, 20, 2, '2024-09-16 13:40:15'),
	(143, 23, 6, '2024-09-24 14:11:12');

-- tablo yapısı dökülüyor eticarets.sifirlamakodlari
CREATE TABLE IF NOT EXISTS `sifirlamakodlari` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `sifirlama_rakami` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `sifirlamakodlari_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.sifirlamakodlari: ~3 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `sifirlamakodlari` (`id`, `UserId`, `sifirlama_rakami`) VALUES
	(27, 22, 70264874),
	(30, 23, 91633139),
	(33, 16, 20341215);

-- tablo yapısı dökülüyor eticarets.siparisler
CREATE TABLE IF NOT EXISTS `siparisler` (
  `id` int NOT NULL AUTO_INCREMENT,
  `user_id` int NOT NULL,
  `product_id` int NOT NULL,
  `siparis_tarihi` datetime NOT NULL,
  `SiparisDurumu` enum('Sipariş Onaylandı','Kargoya Verildi','Teslim Edildi','İptal Edildi') DEFAULT 'Sipariş Onaylandı',
  `iptalet` bit(1) DEFAULT (0),
  PRIMARY KEY (`id`),
  KEY `user_id` (`user_id`),
  KEY `product_id` (`product_id`),
  CONSTRAINT `siparisler_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
  CONSTRAINT `siparisler_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=120 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.siparisler: ~109 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `siparisler` (`id`, `user_id`, `product_id`, `siparis_tarihi`, `SiparisDurumu`, `iptalet`) VALUES
	(1, 16, 4, '2024-08-20 14:19:30', 'Teslim Edildi', b'0'),
	(2, 16, 4, '2024-08-20 14:19:35', 'Sipariş Onaylandı', b'0'),
	(3, 16, 15, '2024-08-20 14:19:36', 'Sipariş Onaylandı', b'0'),
	(4, 16, 15, '2024-08-20 14:19:37', 'Sipariş Onaylandı', b'0'),
	(5, 16, 15, '2024-08-20 14:19:38', 'Sipariş Onaylandı', b'0'),
	(6, 16, 6, '2024-08-20 14:19:39', 'Teslim Edildi', b'0'),
	(7, 16, 6, '2024-08-20 14:19:42', 'Sipariş Onaylandı', b'0'),
	(8, 16, 4, '2024-08-20 14:22:12', 'Sipariş Onaylandı', b'0'),
	(9, 16, 6, '2024-08-20 14:23:46', 'Sipariş Onaylandı', b'0'),
	(10, 16, 4, '2024-08-20 14:29:20', 'Sipariş Onaylandı', b'0'),
	(11, 16, 6, '2024-08-20 15:51:20', 'Kargoya Verildi', b'0'),
	(12, 16, 9, '2024-08-20 16:01:05', 'Sipariş Onaylandı', b'0'),
	(13, 16, 15, '2024-08-20 16:02:21', 'Sipariş Onaylandı', b'0'),
	(14, 16, 9, '2024-08-21 11:13:13', 'Sipariş Onaylandı', b'0'),
	(15, 16, 5, '2024-08-21 11:13:15', 'Sipariş Onaylandı', b'0'),
	(16, 10, 4, '2024-08-21 12:26:48', 'Sipariş Onaylandı', b'0'),
	(17, 10, 6, '2024-08-21 12:26:49', 'Sipariş Onaylandı', b'0'),
	(18, 10, 4, '2024-08-21 12:26:50', 'Kargoya Verildi', b'0'),
	(19, 10, 6, '2024-08-21 12:26:51', 'Sipariş Onaylandı', b'0'),
	(20, 10, 4, '2024-08-21 12:26:53', 'Sipariş Onaylandı', b'0'),
	(21, 10, 4, '2024-08-21 12:26:55', 'Sipariş Onaylandı', b'0'),
	(22, 10, 15, '2024-08-21 14:18:22', 'Sipariş Onaylandı', b'0'),
	(23, 10, 15, '2024-08-21 14:34:04', 'Sipariş Onaylandı', b'0'),
	(24, 10, 1, '2024-08-21 14:34:08', 'Teslim Edildi', b'0'),
	(25, 10, 15, '2024-08-21 14:34:09', 'Sipariş Onaylandı', b'0'),
	(26, 10, 4, '2024-08-21 14:34:10', 'Kargoya Verildi', b'0'),
	(27, 10, 4, '2024-08-21 14:34:11', 'Sipariş Onaylandı', b'0'),
	(28, 10, 4, '2024-08-21 14:34:12', 'Sipariş Onaylandı', b'0'),
	(29, 10, 6, '2024-08-21 14:53:46', 'İptal Edildi', b'0'),
	(30, 10, 6, '2024-08-21 15:01:15', 'Sipariş Onaylandı', b'0'),
	(31, 10, 6, '2024-08-21 15:02:49', 'Sipariş Onaylandı', b'0'),
	(32, 10, 4, '2024-08-21 15:02:49', 'Sipariş Onaylandı', b'0'),
	(33, 10, 4, '2024-08-21 15:02:49', 'Sipariş Onaylandı', b'0'),
	(34, 10, 4, '2024-08-21 15:02:49', 'Sipariş Onaylandı', b'0'),
	(35, 10, 4, '2024-08-21 15:15:34', 'Sipariş Onaylandı', b'0'),
	(36, 10, 4, '2024-08-22 13:29:08', 'Sipariş Onaylandı', b'0'),
	(37, 10, 4, '2024-08-22 13:38:20', 'Sipariş Onaylandı', b'0'),
	(38, 10, 4, '2024-08-22 13:39:24', 'Sipariş Onaylandı', b'0'),
	(39, 10, 4, '2024-08-22 13:46:28', 'Sipariş Onaylandı', b'0'),
	(40, 17, 4, '2024-08-22 13:52:10', 'Sipariş Onaylandı', b'0'),
	(41, 10, 13, '2024-08-22 15:38:25', 'Sipariş Onaylandı', b'0'),
	(42, 10, 13, '2024-08-22 15:38:25', 'Sipariş Onaylandı', b'0'),
	(43, 10, 4, '2024-08-22 15:40:10', 'Sipariş Onaylandı', b'0'),
	(44, 10, 4, '2024-08-22 15:40:10', 'Sipariş Onaylandı', b'0'),
	(45, 16, 4, '2024-08-27 11:25:04', 'Sipariş Onaylandı', b'0'),
	(46, 16, 2, '2024-08-27 11:25:04', 'Sipariş Onaylandı', b'0'),
	(47, 16, 4, '2024-08-28 11:32:38', 'Sipariş Onaylandı', b'0'),
	(48, 10, 4, '2024-08-28 16:58:20', 'Sipariş Onaylandı', b'0'),
	(49, 10, 1, '2024-08-28 23:58:12', 'Kargoya Verildi', b'0'),
	(50, 19, 1, '2024-08-31 18:37:16', 'Sipariş Onaylandı', b'0'),
	(51, 10, 4, '2024-09-04 11:26:15', 'Sipariş Onaylandı', b'0'),
	(52, 10, 4, '2024-09-04 11:26:15', 'Sipariş Onaylandı', b'0'),
	(53, 10, 5, '2024-09-05 20:54:57', 'Sipariş Onaylandı', b'0'),
	(54, 10, 7, '2024-09-05 20:54:57', 'Sipariş Onaylandı', b'0'),
	(55, 10, 4, '2024-09-05 20:54:57', 'Sipariş Onaylandı', b'0'),
	(56, 10, 6, '2024-09-05 20:54:57', 'Sipariş Onaylandı', b'0'),
	(57, 10, 7, '2024-09-05 20:54:57', 'Sipariş Onaylandı', b'0'),
	(58, 10, 6, '2024-09-05 21:04:47', 'Teslim Edildi', b'0'),
	(59, 10, 6, '2024-09-05 21:20:38', 'Sipariş Onaylandı', b'0'),
	(60, 10, 5, '2024-09-06 12:11:38', 'Sipariş Onaylandı', b'0'),
	(61, 19, 4, '2024-09-09 12:07:17', 'Sipariş Onaylandı', b'0'),
	(62, 19, 3, '2024-09-09 12:07:21', 'Sipariş Onaylandı', b'0'),
	(63, 19, 5, '2024-09-09 12:11:24', 'Sipariş Onaylandı', b'0'),
	(64, 19, 5, '2024-09-09 12:19:00', 'Sipariş Onaylandı', b'0'),
	(65, 19, 6, '2024-09-09 13:41:54', 'Kargoya Verildi', b'0'),
	(66, 14, 7, '2024-09-09 13:47:23', 'Sipariş Onaylandı', b'0'),
	(67, 14, 5, '2024-09-09 13:47:26', 'Sipariş Onaylandı', b'0'),
	(68, 14, 4, '2024-09-09 13:54:15', 'Sipariş Onaylandı', b'0'),
	(69, 14, 3, '2024-09-09 13:59:09', 'Sipariş Onaylandı', b'0'),
	(70, 19, 6, '2024-09-09 14:00:41', 'Sipariş Onaylandı', b'0'),
	(71, 16, 9, '2024-09-09 14:33:17', 'Sipariş Onaylandı', b'0'),
	(72, 10, 5, '2024-09-09 15:03:23', 'Sipariş Onaylandı', b'0'),
	(73, 10, 7, '2024-09-09 15:03:23', 'Sipariş Onaylandı', b'0'),
	(74, 10, 5, '2024-09-09 15:03:23', 'Teslim Edildi', b'0'),
	(75, 10, 5, '2024-09-09 15:05:05', 'Sipariş Onaylandı', b'0'),
	(76, 10, 8, '2024-09-09 15:16:22', 'Sipariş Onaylandı', b'0'),
	(77, 10, 6, '2024-09-09 15:24:41', 'Sipariş Onaylandı', b'0'),
	(78, 10, 6, '2024-09-09 15:25:38', 'Sipariş Onaylandı', b'0'),
	(79, 10, 7, '2024-09-09 15:32:53', 'Sipariş Onaylandı', b'0'),
	(80, 10, 18, '2024-09-10 11:40:47', 'Sipariş Onaylandı', b'0'),
	(82, 10, 4, '2024-09-10 17:15:14', 'Sipariş Onaylandı', b'0'),
	(83, 10, 3, '2024-09-11 12:14:32', 'Sipariş Onaylandı', b'0'),
	(84, 10, 3, '2024-09-11 15:20:36', 'Sipariş Onaylandı', b'0'),
	(85, 10, 7, '2024-09-11 15:20:36', 'Sipariş Onaylandı', b'0'),
	(86, 10, 3, '2024-09-12 21:16:06', 'Sipariş Onaylandı', b'0'),
	(87, 10, 7, '2024-09-12 21:16:06', 'Sipariş Onaylandı', b'0'),
	(88, 10, 5, '2024-09-12 21:16:06', 'Sipariş Onaylandı', b'0'),
	(89, 10, 5, '2024-09-12 21:16:06', 'Sipariş Onaylandı', b'0'),
	(90, 10, 5, '2024-09-12 21:16:06', 'Sipariş Onaylandı', b'0'),
	(91, 10, 3, '2024-09-12 21:20:43', 'Sipariş Onaylandı', b'0'),
	(92, 15, 6, '2024-09-12 21:33:23', 'Teslim Edildi', b'0'),
	(93, 15, 5, '2024-09-12 21:33:23', 'Sipariş Onaylandı', b'0'),
	(94, 20, 6, '2024-09-13 17:03:30', 'Sipariş Onaylandı', b'0'),
	(95, 20, 6, '2024-09-13 17:03:30', 'Sipariş Onaylandı', b'0'),
	(96, 22, 6, '2024-09-22 18:41:23', 'Sipariş Onaylandı', b'0'),
	(97, 22, 6, '2024-09-22 18:41:23', 'Sipariş Onaylandı', b'0'),
	(98, 23, 6, '2024-09-23 20:55:27', 'Sipariş Onaylandı', b'0'),
	(99, 23, 6, '2024-09-23 20:55:27', 'Sipariş Onaylandı', b'0'),
	(100, 23, 3, '2024-09-23 20:55:27', 'Sipariş Onaylandı', b'0'),
	(101, 10, 8, '2024-09-23 23:32:45', 'Sipariş Onaylandı', b'0'),
	(102, 10, 5, '2024-09-23 23:59:02', 'Sipariş Onaylandı', b'0'),
	(103, 10, 5, '2024-09-23 23:59:02', 'Sipariş Onaylandı', b'0'),
	(104, 10, 3, '2024-09-24 00:28:03', 'Sipariş Onaylandı', b'0'),
	(105, 10, 5, '2024-09-24 00:28:03', 'Sipariş Onaylandı', b'0'),
	(106, 10, 4, '2024-09-24 00:36:46', 'Sipariş Onaylandı', b'0'),
	(107, 10, 3, '2024-09-24 00:40:57', 'Sipariş Onaylandı', b'0'),
	(108, 10, 3, '2024-09-24 00:40:58', 'Sipariş Onaylandı', b'0'),
	(109, 10, 4, '2024-09-24 00:54:28', 'Sipariş Onaylandı', b'0'),
	(110, 10, 4, '2024-09-24 00:54:28', 'Sipariş Onaylandı', b'0'),
	(111, 10, 3, '2024-09-24 01:47:40', 'Sipariş Onaylandı', b'0'),
	(112, 10, 3, '2024-09-24 01:47:40', 'Sipariş Onaylandı', b'0'),
	(113, 10, 5, '2024-09-24 01:47:40', 'Sipariş Onaylandı', b'0'),
	(114, 23, 8, '2024-09-24 02:36:31', 'Sipariş Onaylandı', b'0'),
	(115, 23, 6, '2024-09-24 02:36:31', 'Sipariş Onaylandı', b'0'),
	(116, 10, 4, '2024-09-24 11:07:23', 'Sipariş Onaylandı', b'0'),
	(117, 10, 5, '2024-09-24 11:07:23', 'Sipariş Onaylandı', b'0'),
	(118, 16, 6, '2024-09-24 12:25:05', 'Sipariş Onaylandı', b'0'),
	(119, 23, 5, '2024-09-24 12:42:38', 'Sipariş Onaylandı', b'0');

-- tablo yapısı dökülüyor eticarets.siparislerimdurumu
CREATE TABLE IF NOT EXISTS `siparislerimdurumu` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserID` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `durum` enum('Sipariş Onaylandı','Sipariş Hazırlanıyor','Kargoya Verildi','İptal Edildi') DEFAULT NULL,
  `IptalEtme` bit(1) DEFAULT NULL,
  `SiparisId` int DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `UserID` (`UserID`),
  KEY `ProductID` (`ProductID`),
  KEY `FK_SiparislerimDurumu_Siparis` (`SiparisId`),
  CONSTRAINT `FK_SiparislerimDurumu_Siparis` FOREIGN KEY (`SiparisId`) REFERENCES `siparisler` (`id`) ON DELETE CASCADE,
  CONSTRAINT `siparislerimdurumu_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`id`),
  CONSTRAINT `siparislerimdurumu_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `products` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.siparislerimdurumu: ~0 rows (yaklaşık) tablosu için veriler indiriliyor

-- tablo yapısı dökülüyor eticarets.toplamparalar
CREATE TABLE IF NOT EXISTS `toplamparalar` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Miktar` decimal(18,2) NOT NULL,
  `alma_zamani` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `toplamparalar_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.toplamparalar: ~31 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `toplamparalar` (`Id`, `UserId`, `Miktar`, `alma_zamani`) VALUES
	(1, 19, 1499.00, '2024-09-09 13:56:26'),
	(2, 14, 1628.00, '2024-09-09 13:56:28'),
	(3, 14, 1499.00, '2024-09-09 13:56:29'),
	(4, 14, 1499.00, '2024-09-09 14:01:01'),
	(5, 19, 1499.00, '2024-09-09 14:00:43'),
	(6, 16, 299.00, '2024-09-09 14:33:17'),
	(7, 10, 3127.00, '2024-09-09 15:03:23'),
	(8, 10, 1499.00, '2024-09-09 15:05:05'),
	(9, 10, 29.00, '2024-09-09 15:16:22'),
	(10, 10, 1499.00, '2024-09-09 15:24:41'),
	(11, 10, 1499.00, '2024-09-09 15:25:38'),
	(12, 10, 129.00, '2024-09-09 15:32:53'),
	(13, 10, 12354.00, '2024-09-10 11:40:47'),
	(14, 10, 11222.00, '2024-09-10 11:41:56'),
	(15, 10, 1499.99, '2024-09-10 17:15:14'),
	(16, 10, 1499.00, '2024-09-11 12:14:32'),
	(17, 10, 1628.00, '2024-09-11 15:20:36'),
	(18, 10, 6125.00, '2024-09-12 21:16:06'),
	(19, 10, 1499.00, '2024-09-12 21:20:47'),
	(20, 15, 2998.00, '2024-09-12 21:33:23'),
	(21, 20, 2998.00, '2024-09-13 17:03:30'),
	(22, 22, 2998.00, '2024-09-22 18:41:23'),
	(23, 23, 4497.00, '2024-09-23 20:55:27'),
	(24, 10, 29.00, '2024-09-23 23:32:45'),
	(25, 10, 2998.00, '2024-09-23 23:59:02'),
	(26, 10, 2998.00, '2024-09-24 00:28:03'),
	(27, 10, 1499.99, '2024-09-24 00:36:46'),
	(28, 10, 2998.00, '2024-09-24 00:40:59'),
	(29, 10, 2999.98, '2024-09-24 00:54:28'),
	(30, 10, 4497.00, '2024-09-24 01:47:40'),
	(31, 23, 1528.00, '2024-09-24 02:36:31'),
	(32, 10, 2998.99, '2024-09-24 11:07:23'),
	(33, 16, 1499.00, '2024-09-24 12:25:05'),
	(34, 23, 1499.00, '2024-09-24 12:42:38');

-- tablo yapısı dökülüyor eticarets.urunyorumlari
CREATE TABLE IF NOT EXISTS `urunyorumlari` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserId` int DEFAULT NULL,
  `ProductID` int DEFAULT NULL,
  `Yorum` text,
  `YorumTarihi` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `UserId` (`UserId`),
  KEY `ProductID` (`ProductID`),
  CONSTRAINT `urunyorumlari_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE,
  CONSTRAINT `urunyorumlari_ibfk_2` FOREIGN KEY (`ProductID`) REFERENCES `products` (`id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.urunyorumlari: ~6 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `urunyorumlari` (`id`, `UserId`, `ProductID`, `Yorum`, `YorumTarihi`) VALUES
	(1, 10, 6, 'bu ürün bir harika', '2024-09-11 13:37:28'),
	(2, 10, 6, 'gerçekten çok sevdim', '2024-09-09 14:07:41'),
	(3, 10, 5, 'bu ürün cidden harika ', '2024-09-11 15:46:36'),
	(4, 10, 5, 'bu ürün çok haliteli', '2024-09-11 15:50:26'),
	(11, 10, 6, 'fiyat performans ürünü', '2024-09-23 21:05:47'),
	(12, 16, 6, 'bu ürün harika', '2024-09-24 12:27:02');

-- tablo yapısı dökülüyor eticarets.usercards
CREATE TABLE IF NOT EXISTS `usercards` (
  `id` int NOT NULL AUTO_INCREMENT,
  `UserId` int DEFAULT NULL,
  `cartnumber` bigint NOT NULL DEFAULT (0),
  `cartholder` varchar(255) NOT NULL,
  `ExpiryMonth` int NOT NULL,
  `ExpiryYear` int NOT NULL,
  `CVV` int NOT NULL,
  PRIMARY KEY (`id`),
  KEY `UserId` (`UserId`),
  CONSTRAINT `usercards_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.usercards: ~12 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `usercards` (`id`, `UserId`, `cartnumber`, `cartholder`, `ExpiryMonth`, `ExpiryYear`, `CVV`) VALUES
	(1, 10, 1234985454324532, 'semih yürükcü', 8, 26, 251),
	(2, 19, 1234546212342321, 'Semih Yürükcü', 28, 28, 987),
	(3, 16, 1234564232145432, '13', 21, 25, 321),
	(4, 10, 1342875469853256, 'Semhj ntırev', 12, 26, 254),
	(5, 10, 1234654234664523, 'jıafokl fds', 8, 28, 325),
	(6, 14, 4943141244331348, 'semih yürükcü', 8, 26, 357),
	(7, 15, 1235875665483548, 'tamer kandil', 8, 28, 123),
	(8, 20, 1234123412341234, 'semih ', 8, 28, 123),
	(9, 21, 1234123412341234, 'joni sinss', 8, 28, 123),
	(10, 20, 1234123412351236, 'adsdsa', 8, 29, 123),
	(11, 22, 7894354621351235, 'ramo tıtır', 8, 29, 123),
	(14, 23, 1234123412341234, 'aykut dursun', 8, 29, 123);

-- tablo yapısı dökülüyor eticarets.users
CREATE TABLE IF NOT EXISTS `users` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Kullanici_adi` varchar(50) NOT NULL DEFAULT 'NULL',
  `Sifre` varchar(100) NOT NULL DEFAULT 'NULL',
  `EMail` varchar(100) DEFAULT NULL,
  `PhoneNumber` varchar(20) DEFAULT NULL,
  `Role` bit(1) DEFAULT NULL,
  `Cinsiyet` bit(1) DEFAULT NULL,
  `D_Tarihi` date DEFAULT NULL,
  `Giris_Sayisi` int DEFAULT NULL,
  `Giris_Tarihi` date DEFAULT NULL,
  `Giris_Yapabilirmi` bit(1) DEFAULT NULL,
  `Adres` text,
  `Saticimi` bit(1) DEFAULT (0),
  `Profil_Foto` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- eticarets.users: ~10 rows (yaklaşık) tablosu için veriler indiriliyor
INSERT INTO `users` (`id`, `Kullanici_adi`, `Sifre`, `EMail`, `PhoneNumber`, `Role`, `Cinsiyet`, `D_Tarihi`, `Giris_Sayisi`, `Giris_Tarihi`, `Giris_Yapabilirmi`, `Adres`, `Saticimi`, `Profil_Foto`) VALUES
	(9, 'fddsfsd', '$2a$11$Nqyiz3f4jywssfdLKFlXKOS/UP4aUacXT.CvTfTAC.4DwPj7r2Jv.', 'grgefb@grtrtge', 'dfgrefg', b'0', b'1', '2002-10-19', 3, NULL, b'1', NULL, b'0', NULL),
	(10, 'synsemih', '$2a$11$qj.WxqVoFS5WDAHrbiyhdu3bcKSZcVYnMJC7CWJyshWqgwV.nVDQa', '84q@dafas', '5540141323', b'1', b'1', '2002-10-19', 3, '2024-09-24', b'1', 'dasköhkökökökö', b'1', '9b4cd241-1e29-42a0-8024-77f5a08e5c58.jpg'),
	(11, 'rtyuıo', '$2a$11$3Czarns7fs3qH0RdDwgHLuh6OrQNAT2Cwq4Rz6x5mzLw6.coCDFAy', 'yuy76yku@yutjk', 'tyutjıku', b'0', b'1', '2002-10-19', 0, '2024-08-15', b'0', NULL, b'0', NULL),
	(12, 'hasan ', '$2a$11$TTrnOelyUpd7WfZFP/rszeGl3co/8tQTEKIwtF5brnVOTucSSD2au', 'hasan@gmail.com', '5520367895', b'0', b'1', '2001-08-18', 3, '2024-08-16', b'1', NULL, b'0', NULL),
	(13, 'ömer', '$2a$11$Hp8uC2oN7Q8KIchhOgfAMOTn4FSlCO29nu17FEgQkwx1EQlcmPfee', 'omer@adsdas', '78564231', b'0', b'1', '2002-10-10', 3, '2024-08-16', b'1', NULL, b'0', NULL),
	(14, 'onurr', '$2a$11$pJGifAlZJso1.EisiII26eHwyTdxdaATEebTvdaQLtOl20zwohURG', 'onorrr@gfmkdmbkdf', 'mfknıekmw', b'0', b'1', '2001-10-18', 3, '2024-09-09', b'1', NULL, b'0', '163616df-4bad-4cd8-a76d-862fa02bfb43.png'),
	(15, 'tamer', '$2a$11$GjGrKfn0BQ4nS4Ki6PpBme5z9pV7tmM7UEIR6rrDmweesrVa/h92S', 'tmr.kndl@gmail.com', 'fdksnfvsmkdl', b'0', b'1', '2005-10-19', 3, '2024-09-12', b'1', NULL, b'0', 'e7e858b5-7f44-4125-b80d-f2691f68f378.png'),
	(16, 'selin', '$2a$11$joX9MSDNmxlRwohH0Ic7l.TcE4qp2TRWrqQEh28U1R3dhexcVbKu6', 'wkdarkside1@gmail.com', '789654231', b'0', b'0', '2003-03-08', 3, '2024-09-24', b'1', 'istanbul', b'0', 'aff53e4d-00fb-4af0-890b-98d86e39fed1.png'),
	(17, 'hasann', '$2a$11$1O/hzM1hdTECPvgCvnZdIOJ2QfGFemZsLvfrosNtbCK7hP3k7g4by', '4565446@gmail.com', '789654', b'0', b'1', '2002-10-19', 3, '2024-08-22', b'1', NULL, b'0', NULL),
	(18, 'dasdas', '$2a$11$RstSD/PB94hTDLHOlpOz/exxvVHoNlUHh48NtbOXf5srrZPhfXJIe', 'yurukcusemin337@gmail.com', 'dsa', b'0', b'1', '2002-11-11', 3, '2024-08-22', b'1', NULL, b'0', NULL),
	(19, 'wkdarkii', '$2a$11$s.P14JxlkLZ8rydg8y1NHe2/u2HZoOu2aqRaOMW4JxTPEUzSMyUjm', 'oyunpatladasadsdsamassa@gmail.com', '87546123', b'0', b'1', '2002-10-19', 0, '2024-09-13', b'0', NULL, b'0', 'ebd9fdc1-ceca-4f8e-aaff-070f96e735fb.png'),
	(20, 'osman', '$2a$11$CHDY.I./ZG2l89D7CuoY7e9Qf5XhuVHhX/sFLMlL29VtfYJ4sgJdi', 'oyunpatlamassaouoooı@gmail.com', '554465213', b'0', b'1', '2002-10-19', 3, '2024-09-16', b'1', NULL, b'0', 'f2445bdf-24b7-4c4a-97bd-c0557a76d177.jpeg'),
	(21, 'mordecai', '$2a$11$V5Js6hVd.T0bgI6tP72Tju7fXZxNpmk.rlVlDzUOKuqAC.b2iFUXG', 'dasdasdas@asdadsdas', '78945612', b'0', b'1', '1999-12-31', 3, '2024-09-16', b'1', NULL, b'0', '9fc08198-dec9-4d0b-be3c-7ff8a0657dd3.png'),
	(22, 'ramo', '$2a$11$EFXawmricsgLya4kukULEuqRoTbY.YoCy2rbxwwXKYPVcxLT6t05e', 'dasdaoyunpatlamassa@gmail.com', '789546213', b'0', b'1', '1999-07-19', 3, '2024-09-22', b'1', NULL, b'0', 'ab9ef865-2f3c-4a73-abe9-36717113dcdc.png'),
	(23, 'aykut', '$2a$11$y8soE0u1wEM2HQ5Iq7bWN.nA.ubIEQzPbM3NIffNpZiS.1FVuH/zi', 'oyunpatlamassa@gmail.com', '123', b'0', b'1', '2003-10-19', 3, '2024-09-24', b'1', NULL, b'0', 'b45255f4-98e7-47c5-b3f1-5d53a025164c.jpg');

-- tetikleyici yapısı dökülüyor eticarets.after_users_delete
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `after_users_delete` AFTER DELETE ON `users` FOR EACH ROW INSERT INTO deleted_users (Kullanici_adi, Sifre, EMail, PhoneNumber, Role, Cinsiyet, D_Tarihi, Giris_Sayisi, Giris_Tarihi, Giris_Yapabilirmi)
VALUES (OLD.Kullanici_adi, OLD.Sifre, OLD.EMail, OLD.PhoneNumber, OLD.Role, OLD.Cinsiyet, OLD.D_Tarihi, OLD.Giris_Sayisi, OLD.Giris_Tarihi, OLD.Giris_Yapabilirmi)//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- tetikleyici yapısı dökülüyor eticarets.update_satisa_koyulan_urun_sayisi
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `update_satisa_koyulan_urun_sayisi` AFTER INSERT ON `sellers` FOR EACH ROW BEGIN
    DECLARE toplam INT;

    -- UserID'ye göre toplam satisa koyulan urun sayisini hesapla
    SELECT COUNT(*) INTO toplam
    FROM Sellers
    WHERE UserID = NEW.UserID;

    -- SatisaKoyulanUrunSayisi tablosunu güncelle veya ekle
    IF EXISTS (SELECT 1 FROM SatisaKoyulanUrunSayisi WHERE UserID = NEW.UserID) THEN
        UPDATE SatisaKoyulanUrunSayisi
        SET ToplamSatisaKoyulanUrunSayisi = toplam
        WHERE UserID = NEW.UserID;
    ELSE
        INSERT INTO SatisaKoyulanUrunSayisi (UserID, ToplamSatisaKoyulanUrunSayisi)
        VALUES (NEW.UserID, toplam);
    END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

-- tetikleyici yapısı dökülüyor eticarets.update_satisa_koyulan_urun_sayisi_after_delete
SET @OLDTMP_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';
DELIMITER //
CREATE TRIGGER `update_satisa_koyulan_urun_sayisi_after_delete` AFTER DELETE ON `sellers` FOR EACH ROW BEGIN
    DECLARE toplam INT;

    -- UserID'ye göre toplam satisa koyulan urun sayisini tekrar hesapla
    SELECT COUNT(*) INTO toplam
    FROM Sellers
    WHERE UserID = OLD.UserID;

    -- Eğer toplam 0 ise, kaydı tamamen sil
    IF toplam = 0 THEN
        DELETE FROM SatisaKoyulanUrunSayisi WHERE UserID = OLD.UserID;
    ELSE
        UPDATE SatisaKoyulanUrunSayisi
        SET ToplamSatisaKoyulanUrunSayisi = toplam
        WHERE UserID = OLD.UserID;
    END IF;
END//
DELIMITER ;
SET SQL_MODE=@OLDTMP_SQL_MODE;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
