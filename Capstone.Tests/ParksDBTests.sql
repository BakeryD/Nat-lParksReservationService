-- EMPTY OUT DB
Delete From reservation;
Delete From site;
Delete From campground;
Delete From park;

-- Parks (3)
SET identity_insert park ON;
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description)
VALUES (1, 'Acadia', 'Maine', '1919-02-26', 47389, 2563129, 'Covering most of Mount Desert Island and other coastal islands, Acadia features the tallest mountain on the Atlantic coast of the United States, granite peaks, ocean shoreline, woodlands, and lakes. There are freshwater, estuary, forest, and intertidal habitats.');
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description)
VALUES (2, 'Arches',	'Utah', '1929-04-12', 76518,	1284767, 'This site features more than 2,000 natural sandstone arches, including the famous Delicate Arch. In a desert climate, millions of years of erosion have led to these structures, and the arid ground has life-sustaining soil crust and potholes, which serve as natural water-collecting basins. Other geologic formations are stone columns, spires, fins, and towers.');
INSERT INTO park (park_id, name, location, establish_date, area, visitors, description)
VALUES (3, 'Cuyahoga Valley', 'Ohio', '2000-10-11',32860,	2189849, 'This park along the Cuyahoga River has waterfalls, hills, trails, and exhibits on early rural living. The Ohio and Erie Canal Towpath Trail follows the Ohio and Erie Canal, where mules towed canal boats. The park has numerous historic homes, bridges, and structures, and also offers a scenic train ride.');
SET identity_insert park OFF;

--Campgounds (4)
SET identity_insert campground ON;
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (1, 1, 'Blackwoods', 1, 12, 35.00);
INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (2, 1, 'Seawall', 5, 9, 30.00);

INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (3, 2, 'Juniper Group Site', 1, 12, 250.00);

INSERT INTO campground (campground_id, park_id, name, open_from_mm, open_to_mm, daily_fee) VALUES (4, 3, 'The Unnamed Primitive Campsites', 5, 11, 20.00);
SET identity_insert campground OFF;

-- Sites (6)
SET identity_insert site ON;
INSERT INTO site (site_id, site_number, campground_id) VALUES (1, 1, 1);
INSERT INTO site (site_id, site_number, campground_id) VALUES (2, 2, 1);
INSERT INTO site (site_id, site_number, campground_id) VALUES (3, 3, 2);

INSERT INTO site (site_id, site_number, campground_id, utilities) VALUES (4, 4, 3, 1);

INSERT INTO site (site_id, site_number, campground_id, accessible) VALUES (5, 5, 4, 1);
INSERT INTO site (site_id, site_number, campground_id, accessible, utilities) VALUES (6, 6, 4, 1, 1);
SET identity_insert site OFF;

-- Reservations (13)
SET identity_insert reservation ON;
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (1, 1,  'Smith Family Reservation',		'2018-06-20', '2018-06-24');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (2, 1,  'Lockhart Family Reservation',	'2018-06-25', '2018-06-30');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (3, 2,  'Jobs Family Reservation',		'2018-06-20', '2018-06-21');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (4, 2,  'Cook Family Reservation',		'2018-06-22', '2018-06-24');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (5, 3,  'Gates Reservation',				'2018-05-20', '2018-05-30');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (6, 3,  'Satya Nedella',					'2018-06-20', '2018-06-30');

INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (7, 4,  'Scott Gutherie',				'2018-06-25', '2018-06-30');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (8, 4,  'Marisa Mayer',					'2018-06-20', '2018-06-24');

INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (9, 5,  'Beth Mooney',					'2018-05-20', '2018-05-23');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (10, 5, 'Bill Board',					'2018-06-20', '2018-06-30');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (11, 5, 'Bill Loney',					'2018-05-25', '2018-05-30');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (12, 6, 'Simpson Family',				'2018-06-20', '2018-06-30');
INSERT INTO reservation (reservation_id, site_id, name, from_date, to_date) VALUES (13, 6, 'Smith Family',					'2018-05-20', '2018-05-30');
SET identity_insert reservation OFF;