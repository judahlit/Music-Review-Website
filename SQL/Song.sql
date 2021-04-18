SELECT * FROM song;

SELECT * FROM songArtist;

SELECT * FROM songGenre;

SELECT * FROM song WHERE id = 1;


INSERT INTO song(title, dateOfRelease, trackId, score, albumId)
VALUES(
    'Encounter Like a Rendezvous (in Another World)',
    '2017-01-23',
    5,
    8.798,
    (SELECT
            id
        FROM
            album
        WHERE
            title = 'Somewhere Not in This World' AND
            dateOfRelease = '2017-01-21')
);


INSERT INTO songArtist(songId, artistId)
VALUES(
    (SELECT
            id
        FROM
            song
        WHERE
            title = 'Encounter Like a Rendezvous (in Another World)' AND
            dateOfRelease = '2017-01-23'),
    (SELECT
            id
        FROM
            artist
        WHERE
            artistName = 'Taishi')
);


INSERT INTO songGenre(songId, genre)
VALUES(
    (SELECT
            id
        FROM
            song
        WHERE
            title = 'Encounter Like a Rendezvous (in Another World)' AND
            dateOfRelease = '2017-01-23'),
    'piano'
);