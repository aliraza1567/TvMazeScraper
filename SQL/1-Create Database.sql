IF DB_ID('TvMaze') IS NOT NULL
BEGIN
  PRINT 'Database TvMaze already Exists'
END
ELSE
BEGIN
  CREATE DATABASE [TvMaze];
  PRINT 'Database TvMaze has been created'
END

