SELECT
    *
FROM
    album;


SELECT
    *
FROM
    albumArtist;


SELECT
    id
FROM
    album
WHERE
    title = 'Somewhere Not in This World' AND
    dateOfRelease = '2017-01-21';



INSERT INTO
    album(title, dateOfRelease, score, img)
VALUES(
        'Aheiehaih',
        '2000-06-21',
        2.343,
        NULL
    );


INSERT INTO
    albumArtist(albumId, artistId)
VALUES(
        (SELECT
            id
        FROM
            album
        WHERE
            title = 'Somewhere Not in This World' AND
            dateOfRelease = '2017-01-21'),
        (SELECT
            id
        FROM
            artist
        WHERE
            artistName = 'Taishi')
    );


DELETE FROM
    album;


DELETE FROM
    albumArtist;