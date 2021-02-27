CREATE TABLE t_sede (
  `idsede` INT NOT NULL AUTO_INCREMENT,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_direccion` VARCHAR(200) DEFAULT NULL,
  `v_telefono` VARCHAR(10) DEFAULT NULL,
  `v_celular` VARCHAR(10) DEFAULT NULL,
  `v_img` BLOB DEFAULT NULL,
  `v_img_ruta` VARCHAR(500) DEFAULT NULL,
  `v_correo` VARCHAR(50) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  PRIMARY KEY (`idsede`)
);

CREATE TABLE t_nivel (
  `idnivel` INT NOT NULL AUTO_INCREMENT,
  `idsede` INT NOT NULL,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_descripcion` VARCHAR(200) DEFAULT NULL,
  `v_img` BLOB DEFAULT NULL,
  `v_img_ruta` VARCHAR(500) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idsede`) REFERENCES t_sede(`idsede`),
  PRIMARY KEY (`idnivel`)
);

CREATE TABLE t_grado (
  `idgrado` INT NOT NULL AUTO_INCREMENT,
  `idnivel` INT NOT NULL,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_descripcion` VARCHAR(200) DEFAULT NULL,
  `v_img` BLOB DEFAULT NULL,
  `v_img_ruta` VARCHAR(500) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idnivel`) REFERENCES t_nivel(`idnivel`),
  PRIMARY KEY (`idgrado`)
);

CREATE TABLE t_curso (
  `idcurso` INT NOT NULL AUTO_INCREMENT,
  `idgrado` INT NOT NULL,
  `idnivel` INT NOT NULL,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_descripcion` VARCHAR(100) DEFAULT NULL,
  `v_imagen` BLOB DEFAULT NULL,
  `v_imagen_ruta` VARCHAR(500) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idgrado`) REFERENCES t_grado(`idgrado`),
  FOREIGN KEY (`idnivel`) REFERENCES t_nivel(`idnivel`),
  PRIMARY KEY (`idcurso`)
);

CREATE TABLE t_clase (
  `idclase` INT NOT NULL AUTO_INCREMENT,
  `idcurso` INT NOT NULL,
  `i_semana` INT NOT NULL,
  `v_nombre` VARCHAR(100) NOT NULL,
  `v_descripcion` VARCHAR(500) DEFAULT NULL,
  `v_ruta_enlace` VARCHAR(500) NOT NULL,
  `v_ruta_video` VARCHAR(500) NOT NULL,
  `i_categoria` INT NOT NULL,
  `v_imagen` BLOB DEFAULT NULL,
  `v_imagen_ruta` VARCHAR(500) DEFAULT NULL,
  `i_orden` INT DEFAULT NULL,
  `b_estado` bit(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idcurso`) REFERENCES t_curso(`idcurso`),
  PRIMARY KEY (`idclase`)
);

CREATE TABLE t_usuario (
  `idusuario` INT NOT NULL AUTO_INCREMENT,
  `idnivel` INT NOT NULL,
  `idgrado` INT NOT NULL,
  `idsede` INT NOT NULL,
  `idseccion` INT NULL,
  `idprovincia` INT NULL,
  `idciudad` INT NULL,
  `iddistrito` INT NULL,
  `v_nombres` VARCHAR(50) NOT NULL,
  `v_apellido_materno` VARCHAR(45) NOT NULL,
  `v_apellido_paterno` VARCHAR(45) NOT NULL,
  `v_direccion` VARCHAR(100) DEFAULT NULL,
  `i_genero` CHAR(1) DEFAULT NULL,
  `v_correo` VARCHAR(50) DEFAULT NULL,
  `v_imagen` BLOB DEFAULT NULL,
  `v_imagen_ruta` VARCHAR(500) DEFAULT NULL,
  `v_imagen_dni_ruta_1` VARCHAR(500) DEFAULT NULL,
  `v_imagen_dni_ruta_2` VARCHAR(500) DEFAULT NULL,
  `v_celular1` VARCHAR(10) DEFAULT NULL,
  `v_celular2` VARCHAR(10) DEFAULT NULL,
  `i_tipodocumento` INT NULL,
  `v_documento` VARCHAR(20) NULL,
  `d_fechanac` DATE DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
   FOREIGN KEY (`idsede`) REFERENCES t_sede(`idsede`),
   FOREIGN KEY (`idnivel`) REFERENCES t_nivel(`idnivel`),
   FOREIGN KEY (`idgrado`) REFERENCES t_grado(`idgrado`),
  PRIMARY KEY (`idusuario`)
);

CREATE TABLE t_acceso (
  `idacceso` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `i_tipo_usuario` INT NOT NULL,
  `v_usuario` VARCHAR(45) NOT NULL,
  `v_clave` VARCHAR(45) NOT NULL,
  `v_token` VARCHAR(300) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idacceso`)
);

CREATE TABLE t_archivo (
  `idarchivo` INT NOT NULL AUTO_INCREMENT,
  `idclase` INT NOT NULL,
  `idusuario` INT NOT NULL,
  `v_nombre` VARCHAR(50) DEFAULT NULL,
  `v_enlace` VARCHAR(500) DEFAULT NULL,
  `i_tipo_archivo` INT DEFAULT NULL,
  `i_puntaje_minimo` INT NOT NULL,
  `i_puntaje_maximo` INT NOT NULL,
  `d_fecha_inicio` DATETIME NOT NULL,
  `d_fecha_fin` DATETIME NOT NULL,
  `v_imagen` BLOB NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idclase`) REFERENCES t_clase(`idclase`),
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idarchivo`)
);

/*
en adelante se podria cambiar la tabla t_archivo
CREATE TABLE t_actividad_profesor (
  `id_actividad_profesor` INT NOT NULL AUTO_INCREMENT,
  `idclase` INT NOT NULL,
  `idusuario` INT NOT NULL,
  `idcurso` INT NOT NULL,
  `idgrado` INT NOT NULL,
  `i_tipo_examen` INT NOT NULL,
  `v_nombre` VARCHAR(50) NOT NULL,
  `v_descripcion` VARCHAR(150) DEFAULT NULL,
  `i_puntaje_minimo` INT NOT NULL,
  `i_puntaje_maximo` INT NOT NULL,
  `i_tiempo` INT NOT NULL,
  `i_intentos` INT NOT NULL,
  `t_hora_examen` TIME NOT NULL,
  `d_fecha_examen` DATE NOT NULL,
  `i_cantidad_preguntas` INT NOT NULL,
  `i_total_puntaje` INT NOT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  FOREIGN KEY (`idclase`) REFERENCES t_clase(`idclase`),
  FOREIGN KEY (`idcurso`) REFERENCES t_curso(`idcurso`),
  FOREIGN KEY (`idgrado`) REFERENCES t_grado(`idgrado`),
  PRIMARY KEY (`id_actividad_profesor`)
);
*/

CREATE TABLE t_archivo_detalle (
  `idarchivodetalle` INT NOT NULL AUTO_INCREMENT,
  `idarchivo` INT NOT NULL,
  `idusuario` INT NOT NULL,
  `v_imagen` BLOB DEFAULT NULL,
  `i_nota` INT DEFAULT NULL,
  `v_observacion` VARCHAR(500) DEFAULT NULL,
  `v_enlace` VARCHAR(500) NOT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idarchivo`) REFERENCES t_archivo(`idarchivo`),
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idarchivodetalle`)
);

CREATE TABLE t_calificacion (
  `idcalificacion` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `i_tipo_nota` INT NOT NULL, -- si es de la tabla actividad o archivo, etc | si es nota de examen, sera cat. 1, si no 2
  `idnota` INT NOT NULL, -- el id de la tabla actividad(examen) (1), archivo(tarea) (2)
  `v_observacion` VARCHAR(500) DEFAULT NULL,
  `b_estado` BIT(1) NOT NULL,
  `dt_fecharegistro` DATETIME NOT NULL,
  FOREIGN KEY (`idusuario`) REFERENCES t_usuario(`idusuario`),
  PRIMARY KEY (`idcalificacion`)
);

-- ----------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_insertar_alumno`
  ( IN _idnivel INT
    ,IN _idgrado INT
    ,IN _idsede INT
    ,IN _nombres VARCHAR(50)
    ,IN _apellido_materno VARCHAR(50)
    ,IN _apellido_paterno VARCHAR(50)
    ,IN _genero CHAR(1)
    ,IN _correo VARCHAR(50)
    ,IN _estado BIT
    ,IN _fecha_registro DATETIME)
BEGIN

    INSERT INTO t_usuario(
        idnivel
        ,idgrado
        ,idsede
        ,v_nombres
        ,v_apellido_materno
        ,v_apellido_paterno
        ,i_genero
        ,v_correo
        ,b_estado
        ,dt_fecharegistro)
    VALUES (
        _idnivel
        ,_idgrado
        ,_idsede
        ,_nombres
        ,_apellido_materno
        ,_apellido_paterno
        ,_genero
        ,_correo
        ,_estado
        ,_fecha_registro
       );
        
    SELECT LAST_INSERT_ID() as '_result';

END$$
DELIMITER ;

-- ----------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_insertar_usuario`
  (IN _idusuario INT
    , IN _tipo_usuario INT
    , IN _usuario VARCHAR(45)
    , IN _clave VARCHAR(45)
    , IN _token VARCHAR(300)
    , IN _estado BIT
    , IN _fecha_registro DATETIME)
BEGIN

IF EXISTS(SELECT idusuario FROM t_acceso WHERE v_usuario = _usuario AND b_estado = 1) THEN
BEGIN
  SELECT 0 as '_result';
END;
ELSE
BEGIN     
    INSERT into t_acceso(
        idusuario
        , i_tipo_usuario
        , v_usuario
        , v_clave
        , v_token
        , b_estado
        , dt_fecharegistro)
  values (
        _idusuario
        , _tipo_usuario
        , _usuario
        , _clave
        , _token
        , _estado
        , _fecha_registro);
        
    SELECT LAST_INSERT_ID() as '_result';
END;
END IF;

END$$
DELIMITER ;

-- -------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_obtener_acceso`(IN _usuario VARCHAR(45), IN _clave varchar(45))
BEGIN
  IF EXISTS(SELECT idusuario FROM T_ACCESO WHERE v_usuario = _usuario AND v_clave = _clave) THEN
  BEGIN
    SELECT idusuario as '_result' FROM T_ACCESO WHERE v_usuario = _usuario AND v_clave = _clave AND b_estado = 1;
    END;
    ELSE 
    SELECT 0 as '_result';
    BEGIN
    END;
    END IF;
END$$
DELIMITER ;

-- --------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_obtener_usuario`(IN _idusuario int)
BEGIN
  select 
        u.idnivel
        ,u.idusuario
        ,u.idgrado
        ,u.idsede
        ,u.idseccion
        ,u.v_nombres
        ,u.v_apellido_paterno
        ,u.v_apellido_materno
        ,u.v_correo
        ,a.i_tipo_usuario
  FROM t_usuario u
    LEFT JOIN t_acceso a ON a.idusuario = u.idusuario
    LEFT JOIN t_nivel n ON n.idnivel = u.idnivel
    LEFT JOIN t_grado g ON g.idgrado = u.idgrado
    LEFT JOIN t_sede sed ON sed.idsede = u.idsede
    WHERE (u.idusuario = _idusuario OR _idusuario = 0) AND u.b_estado = 1;
END$$
DELIMITER ;

-- --------------------------------------------------------------------------
DELIMITER $$
CREATE PROCEDURE `sp_listar_usuario`(IN _usuario VARCHAR(25), IN _tipousuario INT)
BEGIN
  select 
         u.idnivel
        ,u.idgrado
        ,u.idusuario
        ,u.idsede
        ,u.idseccion
        ,a.v_usuario
        ,u.v_nombres
        ,u.v_apellido_paterno
        ,u.v_apellido_materno
        ,u.v_correo
        ,a.i_tipo_usuario
  FROM t_usuario u
    LEFT JOIN t_acceso a ON a.idusuario = u.idusuario
    LEFT JOIN t_nivel n ON n.idnivel = u.idnivel
    LEFT JOIN t_grado g ON g.idgrado = u.idgrado
    LEFT JOIN t_sede sed ON sed.idsede = u.idsede
    WHERE (a.v_usuario like concat('%', _usuario, '%') OR _usuario = "vacio") 
  AND (a.i_tipo_usuario = _tipousuario OR _tipousuario = 0)
    AND u.b_estado = 1 AND a.b_estado = 1;
END$$
DELIMITER ;

-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_insertar_clase`(
     IN _nombre VARCHAR(100)
    , IN _descripcion VARCHAR(500)
    , IN _rutaenlace VARCHAR(500)
    , IN _rutavideo VARCHAR(500)
    , IN _categoria INT
    , IN _idcurso INT
    , IN _semana INT
    , IN _imagen BLOB
    , IN _imagenruta VARCHAR(500)
    , IN _orden INT
    , IN _estado BIT
    , IN _fecharegistro DATETIME)
BEGIN
  INSERT INTO t_clase(
         idcurso
        , i_semana
        , v_nombre
        , v_descripcion
        , v_ruta_enlace
        , v_ruta_video
        , i_categoria
        , v_imagen
        , v_imagen_ruta
        , i_orden
        , b_estado
        , dt_fecharegistro)
    VALUES (
          _idcurso
        , _semana
        , _nombre
        , _descripcion
        , _rutaenlace
        , _rutavideo
        , _categoria
        , _imagen
        , _imagenruta
        , _orden
        , _estado
        , _fecharegistro);

SELECT LAST_INSERT_ID() as '_result';

END$$
DELIMITER ;

-- -------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_listar_curso`(IN _idgrado INT,IN _idnivel INT, IN _tipo_usuario INT)
BEGIN

  SELECT 
     cu.idcurso
    ,cu.idgrado
    ,cu.idnivel
    ,n.v_nombre as 'nombreNivel'
    ,g.v_nombre as 'nombreGrado'
    ,cu.v_nombre
    ,cu.v_descripcion
    ,cu.v_imagen
    ,cu.v_imagen_ruta
    ,cu.b_estado
    ,DATE_FORMAT(cu.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
  
  FROM t_curso cu
  LEFT JOIN t_nivel n ON cu.idnivel = n.idnivel
    LEFT JOIN t_grado g ON cu.idgrado = g.idgrado
      WHERE
          cu.idgrado = _idgrado 
            AND cu.b_estado = 1
            AND cu.idnivel = _idnivel;

END$$
DELIMITER ;

-- -------------------------------------------------------------------------
-- 1 admin
-- 2 profesor
-- 3 alumno

DELIMITER $$
CREATE PROCEDURE `sp_listar_clase_curso`(IN _idcurso int, IN _tipo_usuario INT)
BEGIN

IF (_tipo_usuario = 3 OR _tipo_usuario = 2) THEN
BEGIN

  SELECT 
          cl.idclase
        , cl.idcurso
        , cl.i_semana
        , cu.v_nombre
        , cl.v_nombre as 'nombrecurso'
        , cl.v_descripcion
        , cl.v_ruta_enlace
        , cl.v_ruta_video
        , cl.i_categoria
        , cl.v_imagen
        , cl.v_imagen_ruta 
  FROM t_clase cl
  LEFT JOIN t_curso cu ON cl.idcurso = cu.idcurso
    WHERE
        cl.idcurso = _idcurso AND cl.b_estado = 1;
  END;
  ELSE
  BEGIN

  SELECT 
          cl.idclase
        , cl.idcurso
        , cl.i_semana
        , cu.v_nombre
        , cl.v_nombre as 'nombrecurso'
        , cl.v_descripcion
        , cl.v_ruta_enlace
        , cl.v_ruta_video
        , cl.i_categoria
        , cl.v_imagen
        , cl.v_imagen_ruta 
  FROM t_clase cl
   INNER JOIN t_curso cu ON cl.idcurso = cu.idcurso
    WHERE
        cl.idcurso = _idcurso AND cl.b_estado = 1;

END;
END IF;

END$$
DELIMITER ;

-- -------------------------------------------------------------------------


DELIMITER $$
CREATE PROCEDURE `sp_insertar_archivo`(
      IN _idclase INT
    , IN _idusuario INT
    , IN _nombre VARCHAR(50)
    , IN _enlace VARCHAR(500)
    , IN _tipoarchivo INT
    , IN _puntajeminimo INT
    , IN _puntajemaximo INT
    , IN _fechainicio DATETIME
    , IN _fechafin DATETIME
    , IN _imagen BLOB
    , IN _estado BIT
    , IN _fecharegistro DATETIME)
BEGIN
  INSERT INTO t_archivo(
         idclase
        , idusuario
        , v_enlace
        , v_nombre
        , i_tipo_archivo
        , i_puntaje_minimo
        , i_puntaje_maximo
        , d_fecha_inicio
        , d_fecha_fin
        , v_imagen
        , b_estado
        , dt_fecharegistro)
  VALUES(
          _idclase
        , _idusuario
        , _enlace
        , _nombre
        , _tipoarchivo
        , _puntajeminimo
        , _puntajemaximo
        , _fechainicio
        , _fechafin
        , _imagen
        , _estado
        , _fecharegistro);
END$$
DELIMITER ;


-- -------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_obtener_archivo`(IN _idclase INT)
BEGIN
    SELECT 
          a.idarchivo
        , a.idclase
        , a.v_enlace
        , c.v_nombre as 'nombreClase'
        , a.i_tipo_archivo
        , a.i_puntaje_minimo
        , a.i_puntaje_maximo
        , DATE_FORMAT(a.d_fecha_inicio, '%d-%m-%Y') as 'd_fecha_inicio'
        , DATE_FORMAT(a.d_fecha_fin, '%d-%m-%Y') as 'd_fecha_fin'
        , a.v_nombre as 'v_imagen'
        , a.b_estado
        , DATE_FORMAT(a.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
        , c.i_semana
    FROM t_archivo a LEFT JOIN t_clase c ON a.idclase = c.idclase
    WHERE a.b_estado = 1 AND (a.idclase = _idclase OR _idclase = 0);
END$$
DELIMITER ;

-- --------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_insertar_archivo_detalle`(
      IN _idarchivo INT
    , IN _idusuario INT
    , IN _imagen BLOB
    , IN _nota INT
    , IN _observacion VARCHAR(500)
    , IN _enlace VARCHAR(500)
    , IN _fecharegistro DATETIME)
BEGIN
  INSERT INTO t_archivo_detalle (
          idarchivo
        , idusuario
        , v_imagen
        , i_nota
        , v_observacion
        , b_estado
        , v_enlace
        , dt_fecharegistro)
  VALUES (
          _idarchivo
        , _idusuario
        , _imagen
        , _nota
        , _observacion
        , 1
        , _enlace
        , _fecharegistro);
END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/* lista la tarea de cada alumno */
DELIMITER $$
CREATE PROCEDURE `sp_listar_archivo_detalle`(IN _idarchivo INT, IN _idusuario INT)
BEGIN
  SELECT
          ad.idarchivodetalle
        , ad.idarchivo
        , ad.idusuario
        , ad.v_imagen
        , ad.i_nota
        , ad.v_observacion
        , ad.v_enlace
  FROM t_archivo_detalle ad
  WHERE ad.b_estado = 1 AND ad.idarchivo = _idarchivo AND ad.idusuario = _idusuario;
END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/* lista la tarea de todos los alumnos */
DELIMITER $$
CREATE PROCEDURE `sp_listar_archivo`(IN _idclase INT, IN _idgrado INT, IN _idnivel INT, IN _idcurso INT)
BEGIN
     SELECT
       ad.idarchivo
      ,0 as 'idclase'
      ,ad.idusuario
      ,u.v_nombres
      ,u.v_apellido_materno
      ,u.v_apellido_paterno
      ,CONCAT_WS('/', n.v_nombre, g.v_nombre)  as 'nombreGrado'
      ,n.v_nombre as 'nombreNivel'
      ,(SELECT c1.v_nombre FROM t_clase c1 WHERE c1.idclase = a.idclase) as 'v_nombre'
      ,ad.v_enlace
      ,a.i_tipo_archivo
      ,0 as 'i_puntaje_minimo'
      ,0 as 'i_puntaje_maximo'
      ,'0' as 'd_fecha_inicio'
      ,'0' as 'd_fecha_fin'
      ,CONCAT_WS(' ',u.v_nombres,u.v_apellido_materno, u.v_apellido_paterno) as 'v_imagen'
      ,'0' as 'b_estado'
      ,DATE_FORMAT(ad.dt_fecharegistro, '%d-%m-%Y') as 'dt_fecharegistro'
      ,ad.idarchivodetalle
      ,ad.v_imagen as 'imagenAlumno'
      ,ad.i_nota
      ,ad.v_observacion
      ,ad.v_enlace as 'enlaceAlumno'
      ,cl.idcurso
      ,cl.v_nombre as 'nombreCurso'

  FROM t_archivo_detalle ad 
  LEFT JOIN t_archivo a ON ad.idarchivo = a.idarchivo
   LEFT JOIN t_usuario u ON u.idusuario = ad.idusuario
    LEFT JOIN t_nivel n ON u.idnivel = n.idnivel
     LEFT JOIN t_grado g ON u.idgrado = g.idgrado
      LEFT JOIN t_clase cl ON cl.idclase = a.idclase
  WHERE ad.b_estado = 1
      AND ((a.idclase = _idclase) OR (_idclase = 0))
        AND ((u.idnivel = _idnivel) OR (_idnivel = 0)) 
          AND ((u.idgrado = _idgrado) OR (_idgrado = 0))
           AND ((cl.idcurso = _idcurso) OR (_idcurso = 0));

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/* actualiza las notas de los alumnos */
DELIMITER $$
CREATE PROCEDURE `sp_actualizar_nota`(
        IN _idarchivodetalle INT
      , IN _nota INT
      , IN _observacion VARCHAR(500)
      , IN _idusuario INT
      , IN _tiponota INT
      , IN _estado BIT(1)
      , IN _fecharegistro DATETIME)
BEGIN
  UPDATE t_calificacion
        SET b_estado = 0
  WHERE idusuario = _idusuario;

  UPDATE t_archivo_detalle
        SET i_nota = _nota
            ,v_observacion = _observacion
  WHERE idarchivodetalle = _idarchivodetalle;

  INSERT INTO t_calificacion
        (idusuario
        ,i_tipo_nota
        ,idnota
        ,b_estado
        ,dt_fecharegistro)
        VALUES
        (_idusuario
        ,_tiponota
        ,_idarchivodetalle
        ,_estado
        ,_fecharegistro);

  SELECT LAST_INSERT_ID() as '_result';

END$$
DELIMITER ;

-- --------------------------------------------------------------------------

/* lista toda las calificaciones del alumno*/

DELIMITER $$
CREATE PROCEDURE `sp_listar_calificacion`(IN _idusuario INT, IN _tiponota INT, IN _idnota INT, IN _isemana INT)
BEGIN
  SELECT
         c.idcalificacion
        ,c.idusuario
        ,u.v_nombres
        ,u.v_apellido_materno
        ,u.v_apellido_paterno
        ,u.idgrado
        ,u.idnivel
        ,n.v_nombre as 'nombreNivel'
        ,g.v_nombre as 'nombreGrado'
        ,c.i_tipo_nota
        ,c.idnota
        ,ad.i_nota
        ,ad.v_observacion
        ,(SELECT cu.v_nombre FROM t_curso cu 
            WHERE cu.idcurso = (SELECT cl.idcurso FROM t_clase cl WHERE cl.idclase = a.idclase)) as 'snombrecurso'
        ,a.idclase
        ,cl.i_semana

  FROM t_calificacion c
   LEFT JOIN t_usuario u ON u.idusuario = c.idusuario
    LEFT JOIN t_nivel n ON u.idnivel = n.idnivel
     LEFT JOIN t_grado g ON u.idgrado = g.idgrado
      LEFT JOIN t_archivo_detalle ad ON c.idnota = ad.idarchivodetalle
       LEFT JOIN t_archivo a ON a.idarchivo = ad.idarchivo
        LEFT JOIN t_clase cl ON cl.idclase = a.idclase
        
    WHERE c.b_estado = 1
        AND (c.i_tipo_nota = _tiponota OR _tiponota = 0) -- solo hay 1 o 2 (1:examen) y (2:tarea)
        AND (c.idnota = _idnota OR _idnota = 0)
        AND (c.idusuario = _idusuario OR _idusuario = 0)
        AND (cl.i_semana = _isemana OR _isemana = 0);
END$$
DELIMITER ;

-- --------------------------------------------------------------------------
-- CREAR 3 SP, uno que me liste los niveles, otro los grados y otro los cursos
CREATE TABLE `t_curso_docente` (
  `idcursodocente` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `idnivel` INT NOT NULL,
  `idgrado` INT NOT NULL,
  `idcurso` INT NOT NULL,
  `b_estado` BIT(1) NOT NULL,
  PRIMARY KEY (`idcursodocente`)
);

-- --------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_insertar_curso_docente`(
    IN _idusuario INT
  , IN _idnivel INT
  , IN _idgrado INT
  , IN _idcurso INT
  , IN _b_estado BIT(1))
BEGIN
  INSERT INTO t_curso_docente(
      idusuario
      ,idnivel
      ,idgrado
      ,idcurso
      ,b_estado)
    VALUES (
      _idusuario
      ,_idnivel
      ,_idgrado
      ,_idcurso
      ,_b_estado);
      
 SELECT LAST_INSERT_ID() as '_result';
    
END$$
DELIMITER ;

-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_listar_curso_docente`(
    IN _tipo_usuario INT
  , IN _idusuario INT
  , IN _idnivel INT
  , IN _idgrado INT
  , IN _idcurso INT)
BEGIN
/* SI ES 1 : ADMINISTRADOR ENTONCES PUEDE VER TODOS LOS PROFESORES 
A QUE GRADO Y NIVEL FUERON ASIGNADOS Y SUS CURSOS QUE TIENEN CON ELLOS */

IF (_tipo_usuario = 1) THEN
    BEGIN
    
    SELECT 
     cd.idcursodocente
    , u.v_nombres
    , CONCAT_WS(u.v_apellido_materno, u.v_apellido_paterno) As 'apellidos'
    , n.v_nombre as 'nombreNivel'
    , g.v_nombre as 'nombreGrado'
    , c.v_nombre as 'nombreCurso'
    , cd.idnivel
    , cd.idgrado
    , cd.idcurso
  FROM t_curso_docente cd
  LEFT JOIN t_usuario u on u.idusuario = cd.idusuario 
    LEFT JOIN t_nivel n on n.idnivel = cd.idnivel
      LEFT JOIN t_grado g on g.idgrado = cd.idgrado
        LEFT JOIN t_curso c on c.idcurso = cd.idcurso
  WHERE ((u.idusuario = _idusuario) OR (_idusuario = 0))
    AND ((cd.idnivel = _idnivel) OR (_idnivel = 0))
      AND ((cd.idgrado = _idgrado) OR (_idgrado = 0))
        AND ((cd.idcurso = _idcurso) OR (_idcurso = 0))
          AND cd.b_estado = 1;

    END;
    ELSEIF (_tipo_usuario = 2) THEN
    BEGIN

    SELECT 
     cd.idcursodocente
    , u.v_nombres
    , CONCAT_WS(u.v_apellido_materno, u.v_apellido_paterno) As 'apellidos'
    , n.v_nombre as 'nombreNivel'
    , g.v_nombre as 'nombreGrado'
    , c.v_nombre as 'nombreCurso'
    , cd.idnivel
    , cd.idgrado
    , cd.idcurso
    FROM t_curso_docente cd
    LEFT JOIN t_usuario u on u.idusuario = cd.idusuario 
      LEFT JOIN t_nivel n on n.idnivel = cd.idnivel
        LEFT JOIN t_grado g on g.idgrado = cd.idgrado
          LEFT JOIN t_curso c on c.idcurso = cd.idcurso
    WHERE ((u.idusuario = _idusuario) OR (_idusuario = 0))
      AND ((cd.idnivel = _idnivel) OR (_idnivel = 0))
        AND ((cd.idgrado = _idgrado) OR (_idgrado = 0))
          AND ((cd.idcurso = _idcurso) OR (_idcurso = 0))
            AND cd.b_estado = 1
    GROUP BY cd.idnivel;

    END;
    ELSEIF (_tipo_usuario = 3) THEN
    BEGIN

    SELECT 
     cd.idcursodocente
    , u.v_nombres
    , CONCAT_WS(u.v_apellido_materno, u.v_apellido_paterno) As 'apellidos'
    , n.v_nombre as 'nombreNivel'
    , g.v_nombre as 'nombreGrado'
    , c.v_nombre as 'nombreCurso'
    , cd.idnivel
    , cd.idgrado
    , cd.idcurso
    FROM t_curso_docente cd
    LEFT JOIN t_usuario u on u.idusuario = cd.idusuario 
      LEFT JOIN t_nivel n on n.idnivel = cd.idnivel
        LEFT JOIN t_grado g on g.idgrado = cd.idgrado
          LEFT JOIN t_curso c on c.idcurso = cd.idcurso
    WHERE ((u.idusuario = _idusuario) OR (_idusuario = 0))
      AND ((cd.idnivel = _idnivel) OR (_idnivel = 0))
        AND ((cd.idgrado = _idgrado) OR (_idgrado = 0))
          AND ((cd.idcurso = _idcurso) OR (_idcurso = 0))
            AND cd.b_estado = 1
    GROUP BY cd.idgrado;

    END;
    ELSEIF (_tipo_usuario = 4) THEN
    BEGIN

    SELECT 
     cd.idcursodocente
    , u.v_nombres
    , CONCAT_WS(u.v_apellido_materno, u.v_apellido_paterno) As 'apellidos'
    , n.v_nombre as 'nombreNivel'
    , g.v_nombre as 'nombreGrado'
    , c.v_nombre as 'nombreCurso'
    , cd.idnivel
    , cd.idgrado
    , cd.idcurso
    FROM t_curso_docente cd
    LEFT JOIN t_usuario u on u.idusuario = cd.idusuario 
      LEFT JOIN t_nivel n on n.idnivel = cd.idnivel
        LEFT JOIN t_grado g on g.idgrado = cd.idgrado
          LEFT JOIN t_curso c on c.idcurso = cd.idcurso
    WHERE ((u.idusuario = _idusuario) OR (_idusuario = 0))
      AND ((cd.idnivel = _idnivel) OR (_idnivel = 0))
        AND ((cd.idgrado = _idgrado) OR (_idgrado = 0))
          AND ((cd.idcurso = _idcurso) OR (_idcurso = 0))
            AND cd.b_estado = 1
    GROUP BY cd.idcurso;

    END;
    END IF;

END$$
DELIMITER ;

-- ----------------------------------------------------------------------------
-- NUEVOS PROCEDIMIENTOS

DELIMITER $$
CREATE PROCEDURE `sp_actualizar_acceso`(
  IN _tipoactualizar INT
  ,IN _idusuario INT
  ,IN _v_usuario VARCHAR(45)
  ,IN _v_clave VARCHAR(45))
BEGIN

    IF (_tipoactualizar = 1) THEN
  BEGIN
    
  UPDATE t_acceso
    SET  
       v_usuario = _v_usuario
      ,v_clave = _v_clave
  WHERE idusuario = _idusuario;

    END;
    ELSE 
    
  UPDATE t_acceso
    SET b_estado = 0 
  WHERE idusuario = _idusuario;

    BEGIN
    END;
    END IF;

END$$
DELIMITER ;

-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_actualizar_clase`(
  IN _tipoactualizar INT
  ,IN _idclase INT
  ,IN _idcurso INT
  ,IN _i_semana INT
  ,IN _v_nombre VARCHAR(100)
  ,IN _v_descripcion VARCHAR(500)
  ,IN _v_ruta_enlace VARCHAR(500)
  ,IN _v_ruta_video VARCHAR(500)
  ,IN _v_imagen_ruta VARCHAR(500))
BEGIN

  IF (_tipoactualizar = 1) THEN
  BEGIN
    
  UPDATE t_clase
    SET  
       idcurso = _idcurso
      ,i_semana = _i_semana
      ,v_nombre = _v_nombre
      ,v_descripcion = _v_descripcion
      ,v_ruta_enlace = _v_ruta_enlace
      ,v_ruta_video = _v_ruta_video
      ,v_imagen_ruta = _v_imagen_ruta
  WHERE idclase = _idclase;

    END;
    ELSE 
    
  UPDATE t_clase
    SET b_estado = 0 
  WHERE idclase = _idclase;

    BEGIN
    END;
    END IF;

  
END$$
DELIMITER ;

-- ----------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_actualizar_archivo`(
   IN _tipoactualizar INT
  ,IN _idarchivo INT
  ,IN _v_nombre VARCHAR(50)
  ,IN _v_enlace VARCHAR(500)
  ,IN _i_tipo_archivo INT
  ,IN _i_puntaje_minimo INT
  ,IN _i_puntaje_maximo INT
  ,IN _d_fecha_inicio DATETIME
  ,IN _d_fecha_fin DATETIME)
BEGIN

  IF (_tipoactualizar = 1) THEN
  BEGIN
    
  UPDATE t_archivo
    SET  
       v_nombre = _v_nombre
      ,v_enlace = _v_enlace
      ,i_tipo_archivo = _i_tipo_archivo
      ,i_puntaje_minimo = _i_puntaje_minimo
      ,i_puntaje_maximo = _i_puntaje_maximo
      ,d_fecha_inicio = _d_fecha_inicio
      ,d_fecha_fin = _d_fecha_fin
  WHERE idarchivo = _idarchivo;

    END;
    ELSE 
    
  UPDATE t_archivo
    SET b_estado = 0 
  WHERE idarchivo = _idarchivo;

    BEGIN
    END;
    END IF;

  
END$$
DELIMITER ;

-- ----------------------------------------------------------------------------

  DELIMITER $$
CREATE PROCEDURE `sp_actualizar_archivo_detalle`(
   IN _tipoactualizar INT
  ,IN _idarchivodetalle INT
  ,IN _idarchivo INT
  ,IN _idusuario INT
  ,IN _i_nota INT
  ,IN _v_observacion VARCHAR(500)
  ,IN _v_enlace VARCHAR(500))
BEGIN

  IF (_tipoactualizar = 1) THEN
  BEGIN
    
  UPDATE t_archivo_detalle
    SET  
       tipoactualizar = _tipoactualizar
      ,idarchivodetalle = _idarchivodetalle
      ,idarchivo = _idarchivo
      ,idusuario = _idusuario
      ,i_nota = _i_nota
      ,v_observacion = _v_observacion
      ,v_enlace = _v_enlace
  WHERE idarchivodetalle = _idarchivodetalle;

    END;
    ELSE 
    
  UPDATE t_archivo_detalle
    SET b_estado = 0 
  WHERE idarchivodetalle = _idarchivodetalle;

    BEGIN
    END;
    END IF;

  
END$$
DELIMITER ;

-- ----------------------------------------------------------------------------
/* NUEVOS PROCEDIMIENTOS 11/01/2021 */

CREATE TABLE IF NOT EXISTS t_pago(
  `idpago` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `v_descripcion` VARCHAR(150) NULL,
  `idnivel` INT NOT NULL,
  `idgrado` INT NOT NULL,
  `idcurso` INT NOT NULL,
  `i_dia` INT NULL, -- idia mes anio del registro del inicio de pago
  `i_mes` INT NULL,
  `i_anio` INT NULL,
  `v_hora` VARCHAR(12) NULL,
  `v_fecha_ini_pago` DATETIME NULL,
  `v_fecha_fin_pago` DATETIME NULL,
  `b_estado` BIT(1) NULL,
  `b_vigente` BIT(1) NULL,
  `b_modificado` BIT(1) NULL,
  `i_usuario_modificado` INT NULL,
  `dt_fecharegistro` DATETIME NULL,
  `dt_fechamodificacion` DATETIME NULL,
  `dt_fechafin` DATETIME NULL,
  FOREIGN KEY (idusuario) REFERENCES t_usuario(idusuario),
PRIMARY KEY (`idpago`));

-- tabla intermediaria que registra los pagos mensuales
CREATE TABLE `t_pago_detalle` (
  `idpagodetalle` INT NOT NULL AUTO_INCREMENT,
  `idusuario` INT NOT NULL,
  `idpago` INT NOT NULL,
  `v_operacion_data` VARCHAR(25) NULL,
  `i_tipopago` INT NULL,
  `i_tipomoneda` INT NULL,
  `v_descripcion` VARCHAR(250) NULL,
  `i_dia` INT NULL,
  `i_mes` INT NOT NULL,
  `i_anio` INT NOT NULL,
  `d_monto` DECIMAL(10,2) NULL,
  `d_igv` DECIMAL(10,2) NULL,
  `d_comision_nombre` DECIMAL(10,2) NULL,
  `v_img_ruta_1` VARCHAR(100) NULL,
  `v_img_ruta_2` VARCHAR(100) NULL,
  `v_pasarela_pago_nombre` VARCHAR(50) NULL,
  `v_token` VARCHAR(500) NULL,
  `b_estado` BIT(1) NOT NULL, -- cual es el estado del pago
  `b_reembolsado` BIT(1) NULL, -- si se devolvio la plata
  `b_activo` BIT(1) NULL,
  `dt_fecharegistro` DATETIME NULL,
  `dt_fechapago` DATETIME NULL,
  `i_usuario_modificado` INT NULL,
  `dt_fechamodificacion` DATETIME NULL,
  PRIMARY KEY (`idpagodetalle`)
);


-- ---------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_actualizar_usuario_clave`;*/
DELIMITER $$
CREATE PROCEDURE `sp_actualizar_usuario_clave` (IN _idusuario INT, IN _nuevaclave VARCHAR(50))
BEGIN

UPDATE t_acceso SET v_clave = _nuevaclave WHERE idusuario = _idusuario;

END$$

DELIMITER ;

-- ---------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_listar_pago`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_pago` (
    IN _cuenta VARCHAR(50)
  , IN _idusuario INT
  , IN _idnivel INT
  , IN _idgrado INT
  , IN _idcurso INT
  , IN _b_vigente BIT(1)
  , IN _nombre VARCHAR(50)
  , IN _fechaini VARCHAR(25)
  , IN _fechafin VARCHAR(25)
  , IN _i_mes INT
  , IN _i_anio INT)
BEGIN
SELECT
   p.idpago
  ,p.idusuario
  ,u.v_nombres
  ,u.v_apellido_paterno
  ,u.v_apellido_materno
  ,CONCAT_WS(' ', u.v_nombres, u.v_apellido_paterno, u.v_apellido_materno) as 'usuario'
  ,p.idnivel
  ,(SELECT v_nombre FROM t_nivel n WHERE n.idnivel = p.idnivel) as 'nivelNombre'
  ,p.idcurso
  ,(SELECT v_nombre FROM t_curso c WHERE c.idcurso = p.idcurso) as 'nombreCurso'
  ,p.v_descripcion
  ,a.v_usuario
  ,p.i_dia
  ,p.i_mes
  ,p.i_anio
  ,p.v_hora
  ,DATE_FORMAT(p.v_fecha_ini_pago, '%d-%m-%Y') as 'v_fecha_ini_pago'
  ,DATE_FORMAT(p.v_fecha_fin_pago, '%d-%m-%Y') as 'v_fecha_fin_pago'
  ,p.b_vigente
  ,DATE_FORMAT(p.dt_fecharegistro, '%d-%m-%Y') as 'v_fecharegistro'
  -- ,p.i_usuario_modificado
  -- ,(SELECT CONCAT_WS(' ', u2.v_nombres, u2.v_apellido_paterno, u2.v_apellido_materno) FROM t_usuario u2 WHERE u2.idusuario = p.i_usuario_modificado) as 'usuario'
  FROM t_pago p
    LEFT JOIN t_usuario u ON p.idusuario = u.idusuario
        LEFT JOIN t_acceso a ON a.idusuario = p.idusuario
  WHERE (p.idusuario = _idusuario OR _idusuario = 0)
      AND (a.v_usuario = _cuenta OR _cuenta = '')
      AND (p.idnivel = _idnivel OR _idnivel = 0)
      AND (p.idcurso = _idcurso OR _idcurso = 0)
      AND (p.idgrado = _idgrado OR _idgrado = 0)
      AND (p.b_vigente = _b_vigente OR _b_vigente = 0)
      AND ((p.dt_fecharegistro BETWEEN DATE_FORMAT(_fechaini, '%d-%m-%Y') AND DATE_FORMAT(_fechafin, '%d-%m-%Y')) OR (_fechaini = 'vacio' AND _fechafin = 'vacio'))
      AND (p.i_mes = _i_mes OR _i_mes = 0)
      AND (p.i_anio = _i_anio OR _i_anio = 0);

END$$
DELIMITER ;

-- ---------------------------------------------------------------------------
/*DROP procedure IF EXISTS `sp_listar_pago_detalle`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_pago_detalle` (
    IN _idpago INT
  , IN _b_activo INT
  , IN _b_estado INT
  , IN _fechaini VARCHAR(25)
  , IN _fechafin VARCHAR(25))
BEGIN
SELECT
   pd.idpagodetalle
  ,pd.idpago
  ,pd.v_operacion_data
  ,pd.i_tipopago
  ,pd.i_tipomoneda
  ,pd.v_descripcion
  ,pd.idusuario as 'i_dia'
  ,pd.i_mes
  ,pd.i_anio
  ,pd.d_monto
  ,pd.v_img_ruta_1
  ,pd.v_img_ruta_2
  ,pd.v_pasarela_pago_nombre
  ,pd.b_estado
  ,DATE_FORMAT(pd.dt_fecharegistro, '%d-%m-%Y') as 'v_fecharegistro'
  ,DATE_FORMAT(pd.dt_fechapago, '%d-%m-%Y') as 'v_dt_fechapago'
  FROM t_pago_detalle pd
  WHERE (pd.idpago = _idpago OR _idpago = 0)
      AND (pd.b_activo = _b_activo OR _b_activo = 99)
      AND (pd.b_estado = _b_estado OR _b_estado = 99)
      AND ((pd.dt_fecharegistro BETWEEN DATE_FORMAT(_fechaini, '%d-%m-%Y') AND DATE_FORMAT(_fechafin, '%d-%m-%Y')) OR (_fechaini = 'vacio' AND _fechafin = 'vacio'))
		ORDER BY pd.i_mes ASC;
END$$
DELIMITER ;

-- ---------------------------------------------------------------------------
/*DROP procedure IF EXISTS `sp_insertar_pago`;*/
DELIMITER $$
CREATE PROCEDURE `sp_registrar_pago` (
    IN _idpago INT
  , IN _idpagodetalle INT
  , IN _idusuario INT
  , IN _idnivel INT
  , IN _idgrado INT
  , IN _idcurso INT
  , IN _v_operacion_data VARCHAR(25)
  , IN _i_tipopago INT
  , IN _i_tipomoneda INT
  , IN _v_descripcion VARCHAR(250)
  , IN _i_mes INT
  , IN _i_anio INT
  , IN _v_hora VARCHAR(12)
  , IN _d_monto DECIMAL(10,2)
  , IN _v_img_ruta_1 VARCHAR(100)
  , IN _v_img_ruta_2 VARCHAR(100)
  , IN _v_fecha_ini_pago VARCHAR(25)
  , IN _b_estado BIT(1)
  , IN _dt_fecharegistro VARCHAR(25)
  , IN _dt_fechafin VARCHAR(25))
BEGIN

DECLARE _result INT DEFAULT 0;
DECLARE _idpagofiltro INT DEFAULT 0;

-- UPDATE t_pago SET b_estado = 0 WHERE idusuario = _idusuario;
IF EXISTS(SELECT idpago FROM t_pago 
  WHERE idusuario = _idusuario
        AND idnivel = _idnivel
        AND idgrado = _idgrado
        AND idcurso = _idcurso) -- esta condicional quitar para colegios como el amigo de mario
THEN
BEGIN
-- si existe ya filas en el pago
-- UPDATE t_pago SET dt_fechafin = _dt_fechafin WHERE idpago = _idpago;
SET _idpagofiltro = (SELECT idpago FROM t_pago 
    WHERE idusuario = _idusuario
        AND idnivel = _idnivel
        AND idgrado = _idgrado
        AND idcurso = _idcurso);


UPDATE t_pago_detalle SET
   v_operacion_data = _v_operacion_data
  ,i_tipopago = _i_tipopago
  ,i_tipomoneda = _i_tipomoneda
  ,v_descripcion = _v_descripcion
  ,d_monto = _d_monto
  ,v_img_ruta_1 = _v_img_ruta_1
  ,v_img_ruta_2 = _v_img_ruta_2
  ,b_estado = _b_estado
  ,b_activo = 1
  ,dt_fechapago = STR_TO_DATE(_dt_fecharegistro ,'%Y-%m-%d')
  WHERE idusuario = _idusuario
        AND idpago = _idpagofiltro
        AND i_mes = _i_mes 
        AND i_anio = _i_anio;

END;
ELSE
BEGIN
-- si no existe el pago del curso
INSERT INTO t_pago(
  idusuario
  ,v_descripcion
  ,idnivel
  ,idgrado
  ,idcurso
  ,i_mes
  ,i_anio
  ,v_hora
  ,v_fecha_ini_pago
  ,b_estado
  ,dt_fecharegistro
)VALUES(
   _idusuario
  ,_v_descripcion
  ,_idnivel
  ,_idgrado
  ,_idcurso
  ,_i_mes
  ,_i_anio
  ,_v_hora
  ,STR_TO_DATE(_v_fecha_ini_pago,'%Y-%m-%d')
  ,1
  ,STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));

  SET _result = LAST_INSERT_ID();

  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,1,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,2,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,3,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,4,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,5,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,6,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,7,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,8,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,9,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,10,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,11,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));
  INSERT INTO t_pago_detalle(idusuario, idpago, i_mes, i_anio,d_monto,b_estado,b_activo, dt_fecharegistro)
  VALUES(_idusuario,_result,12,_i_anio,0,0,0, STR_TO_DATE(_dt_fecharegistro,'%Y-%m-%d'));

  UPDATE t_pago_detalle SET
     v_operacion_data = _v_operacion_data
    ,i_tipopago = _i_tipopago
    ,i_tipomoneda = _i_tipomoneda
    ,v_descripcion = _v_descripcion
    ,d_monto = _d_monto
    ,v_img_ruta_1 = _v_img_ruta_1
    ,v_img_ruta_2 = _v_img_ruta_2
    ,b_estado = _b_estado
    ,b_activo = 1
    ,dt_fechapago = STR_TO_DATE(_dt_fecharegistro ,'%Y-%m-%d')
  WHERE idpago = _result AND i_anio = _i_anio AND i_mes = _i_mes AND idusuario = _idusuario;

END;
END IF;


END$$
DELIMITER ;

-- ---------------------------------------------------------------------------

/*DROP procedure IF EXISTS `sp_pago_notificar`;*/
DELIMITER $$
CREATE PROCEDURE `sp_pago_notificar` (
    IN _idusuario INT
  , IN _i_mes INT
  , IN _fechaacceso VARCHAR(25)
  , IN _fechavalidar VARCHAR(25)) -- este campo es los 25 de cada mes 
BEGIN

IF EXISTS(SELECT idpago FROM t_pago_detalle 
    WHERE (idusuario = _idusuario OR _idusuario = 0)
      AND (i_mes = _i_mes)
      AND DATE(_fechaacceso) >= DATE(_fechavalidar)) THEN
BEGIN
-- si le falta realizar algun pago de algun curso
SELECT 1 as '_result';

END;
ELSE
BEGIN

SELECT 0 as '_result';

END;
END IF;

END$$

DELIMITER ;

-- ---------------------------------------------------------------------------

-- lista la suma de todas las ventas de rango de fechas dicho mes, dicho año
DELIMITER $$
CREATE PROCEDURE `sp_reporte_sumapagos` (
   IN _idnivel INT
  ,IN _idgrado INT
  ,IN _idcurso INT
  ,IN _fechaini VARCHAR(25)
  ,IN _fechafin VARCHAR(25)
  ,IN _i_mes INT
  ,IN _i_anio INT)
BEGIN

SELECT 
   SUM(pd.d_monto) as 'd_monto'
  ,SUM(pd.b_activo) as 'i_pago'
  ,ROUND(AVG(pd.d_monto),1) as 'costo_promedio_dec' -- son los usuarios pagando
FROM t_pago_detalle pd
  INNER JOIN t_pago p ON pd.idpago = p.idpago
WHERE 
      (p.idnivel = _idnivel OR _idnivel = 0)
      AND (p.idcurso = _idcurso OR _idcurso = 0)
      AND (p.idgrado = _idgrado OR _idgrado = 0)
      AND (DATE(pd.dt_fecharegistro) BETWEEN DATE(_fechaini) AND DATE(_fechafin))
      AND (pd.i_mes = _i_mes OR _i_mes = 0)
      AND (pd.i_anio = _i_anio OR _i_anio = 0);

END$$
DELIMITER ;

-- ---------------------------------------------------------------------------

-- listar a los usuarios por curso
DELIMITER $$
CREATE PROCEDURE `sp_listar_usuario_total`(
   IN _idnivel INT
  ,IN _idgrado INT
  ,IN _idcurso INT)
BEGIN

  IF (_idnivel = 1) THEN
  BEGIN
  
    SELECT COUNT(a.idusuario) as 'i_usuario'
    FROM t_acceso a WHERE a.i_tipo_usuario = 3;

  END;
  ELSE 
  BEGIN

  SELECT COUNT(p.idusuario) as 'i_usuario'
    FROM t_pago p WHERE p.b_estado = 1 AND ((p.idcurso = _idcurso) OR (_idcurso = 0));

  END;
  END IF;


END$$
DELIMITER ;

-- ---------------------------------------------------------------------------
-- trae informacion que dice los meses que me falta pagar SOLO USUARIO POR AHORA

/*DROP procedure IF EXISTS `sp_listar_pago_pendiente`;*/
DELIMITER $$
CREATE PROCEDURE `sp_listar_pago_pendiente` (
    IN _idusuario INT
  , IN _idnivel INT
  , IN _idgrado INT
  , IN _idcurso INT
  , IN _b_activo INT)
BEGIN
SELECT
   pd.idpagodetalle
  ,pd.idpago
  ,p.i_dia
  ,pd.i_mes
  ,pd.i_anio
  FROM t_pago_detalle pd
    LEFT JOIN t_pago p ON p.idpago = pd.idpago
  WHERE (pd.idusuario = _idusuario OR _idusuario = 0)
    AND (p.idnivel = _idnivel OR _idnivel = 0)
    AND (p.idcurso = _idcurso OR _idcurso = 0)
    AND (p.idgrado = _idgrado OR _idgrado = 0)
    AND (pd.b_activo = 0 OR _b_activo = 99) ORDER BY pd.i_mes ASC;

END$$
DELIMITER ;

-- ---------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_listar_grado` (IN _idnivel INT)
BEGIN
  SELECT 
  g.idgrado, 
  g.v_nombre 
  FROM t_grado g
    LEFT JOIN t_nivel n ON g.idnivel = n.idnivel
  WHERE g.b_estado = 1 AND g.idnivel = _idnivel ; 
END$$
DELIMITER ;

-- ---------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_actualizar_usuario`( 
   IN _idusuario INT
    ,IN _idnivel INT
    ,IN _idgrado INT
    ,IN _idsede INT
    ,IN _nombres VARCHAR(50)
    ,IN _apellido_materno VARCHAR(50)
    ,IN _apellido_paterno VARCHAR(50)
    ,IN _genero CHAR(1)
    ,IN _correo VARCHAR(50)
    ,IN _estado BIT
    ,IN _fecha_registro DATETIME)
BEGIN
    UPDATE t_usuario
    SET
        idnivel = _idnivel,
        idgrado = _idgrado,
        idsede = _idsede,
        v_nombres = _nombres,
        v_apellido_materno = _apellido_materno,
        v_apellido_paterno = _apellido_paterno,
        i_genero = _genero,
        v_correo = _correo,
        b_estado = _estado,
        dt_fecharegistro = _fecha_registro
    WHERE 
        idusuario = _idusuario;        

END$$
DELIMITER ;

-- ---------------------------------------------------------------------------

DELIMITER $$
CREATE PROCEDURE `sp_listar_nivel` (IN _idnivel INT)
BEGIN
  SELECT 
  n.idnivel, 
  n.v_nombre 
  FROM t_nivel n WHERE n.b_estado = 1; 
END$$
DELIMITER ;

-- ---------------------------------------------------------------------------

-- primer pago segundo pago, etc
-- matricula
-- 8 mensualidades o hasta 8 pagos (quinta mensualidad, segunda mensualidad)
-- seleccionar esos casos
-- los cursos tienen una duracion establecida
-- que puedan selecciona promocion y establecer el tipo de promocion


-- auxiliar 8 pagos
   -- inicial (uno solo)
   -- primaria (uno solo)
-- auxiliar secundaria 6 pagos
-- didactica son 4 pagos
-- terapia 8 pagos
-- estimulacion 8 pagos
-- dividen grupos por fecha de inicio
-- VER fecha de inicio del CURSO es distinto para cada curso

/*
1 Auxiliar Inicial 8 
2 Auxiliar Primaria 8
3 Auxiliar Secundaria 6
4 Didáctica 4
5 Terapia 8
6 Estimulación temprana 8
*/

-- las tareas las toman
-- 16 temas y duran 2 semanas
-- las notas vienen cada dos semanas
-- se suma todo y se divide entre la cantidad de notas.

-- hay el caso que no han podido ingresar al curso, y han dejado a medias el curso.
-- le dejan un trabajo adicional y lo presentan y eso suplanta las notas faltantes.
-- cada mes 4 semenas: auxiliar 4 semanas

-- estimulacion temparana varia los modulos. 
-- terapia varia los modulos

-- auxiliar 1 modulo 2 temas: 
-- cada tema demora 2 sabados o sea dos semanas (auxiliar)
-- cada 4 sabados. 
