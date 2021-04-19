-- Create tables --

CREATE TABLE `Song` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `title` varchar(60) not null,
  `dateOfRelease` date not null,
  `trackId` int,
  `score` decimal,
  `albumId` int
);

CREATE TABLE `SongGenre` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `songId` int not null,
  `genre` varchar(30) not null
);

CREATE TABLE `SongArtist` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `songId` int not null,
  `artistId` int not null
);

CREATE TABLE `Artist` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `artistName` varchar(30) not null,
  `img` blob,
  `description` varchar(1000)
);

CREATE TABLE `Album` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `title` varchar(60) not null,
  `dateOfRelease` date not null,
  `score` decimal,
  `img` blob
);

CREATE TABLE `AlbumArtist` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `albumId` int not null,
  `artistId` int not null
);

CREATE TABLE `Single` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `songId` int not null,
  `img` blob
);

CREATE TABLE `SongScore` (
  `id` int PRIMARY KEY AUTO_INCREMENT,
  `songId` int not null,
  `username` varchar(30) not null,
  `score` decimal not null,
  `review` varchar(2500)
);

CREATE TABLE `AlbumScore` (
  `id` int PRIMARY KEY,
  `albumId` int not null,
  `username` varchar(30) not null,
  `score` decimal not null,
  `review` varchar(2500)
);


-- Add references --

ALTER TABLE `Song` ADD FOREIGN KEY (`albumId`) REFERENCES `Album` (`id`);

ALTER TABLE `SongGenre` ADD FOREIGN KEY (`songId`) REFERENCES `Song` (`id`);

ALTER TABLE `SongArtist` ADD FOREIGN KEY (`songId`) REFERENCES `Song` (`id`);

ALTER TABLE `SongArtist` ADD FOREIGN KEY (`artistId`) REFERENCES `Artist` (`id`);

ALTER TABLE `AlbumArtist` ADD FOREIGN KEY (`albumId`) REFERENCES `Album` (`id`);

ALTER TABLE `AlbumArtist` ADD FOREIGN KEY (`artistId`) REFERENCES `Artist` (`id`);

ALTER TABLE `Single` ADD FOREIGN KEY (`songId`) REFERENCES `Song` (`id`);

ALTER TABLE `SongScore` ADD FOREIGN KEY (`songId`) REFERENCES `Song` (`id`);

ALTER TABLE `AlbumScore` ADD FOREIGN KEY (`albumId`) REFERENCES `Album` (`id`);


-- View tables --

DESCRIBE Song;

DESCRIBE SongGenre;

DESCRIBE SongArtist;

DESCRIBE Artist;

DESCRIBE Album;

DESCRIBE AlbumArtist;

DESCRIBE Single;

DESCRIBE SongScore;

DESCRIBE AlbumScore;


-- Delete tables --

DROP TABLE SongGenre;

DROP TABLE SongArtist;

DROP TABLE Single;

DROP TABLE SongScore;

DROP TABLE AlbumScore;

DROP TABLE Song;

DROP TABLE AlbumArtist;

DROP TABLE Album;

DROP TABLE Artist;