CREATE OR REPLACE PACKAGE PKG_CLIENTE AS

  PROCEDURE listar(
    p_cursor OUT SYS_REFCURSOR
  );

  PROCEDURE insertar(
    p_nrodoc   IN CLIENTE.NRODOC%TYPE,
    p_nombre   IN CLIENTE.NOMBRE%TYPE,
    p_apellido IN CLIENTE.APELLIDO%TYPE
  );

  PROCEDURE actualizar(
    p_codcliente IN NUMBER,
    p_nrodoc     IN VARCHAR2,
    p_nombre     IN VARCHAR2,
    p_apellido   IN VARCHAR2
  );

  PROCEDURE eliminar(
    p_codcliente IN NUMBER
  );

END PKG_CLIENTE;
/



CREATE OR REPLACE PACKAGE BODY PKG_CLIENTE AS

  PROCEDURE listar(
    p_cursor OUT SYS_REFCURSOR
  )
  AS
  BEGIN
    OPEN p_cursor FOR
      SELECT codcliente, nrodoc, nombre, apellido
      FROM cliente
      ORDER BY codcliente;
  END listar;

  PROCEDURE insertar(
    p_nrodoc   IN CLIENTE.NRODOC%TYPE,
    p_nombre   IN CLIENTE.NOMBRE%TYPE,
    p_apellido IN CLIENTE.APELLIDO%TYPE
  )
  IS
    v_count NUMBER;
  BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM cliente
    WHERE nrodoc = p_nrodoc;

    IF v_count > 0 THEN
      RAISE_APPLICATION_ERROR(-20001, 'Ya existe un cliente con el nro documento.');
    END IF;

    IF TRIM(p_nombre) IS NULL THEN
      RAISE_APPLICATION_ERROR(-20002, 'El nombre del cliente no puede estar vacío.');
    END IF;

    IF TRIM(p_apellido) IS NULL THEN
      RAISE_APPLICATION_ERROR(-20003, 'El apellido del cliente no puede estar vacío.');
    END IF;

    INSERT INTO cliente (nrodoc, nombre, apellido)
    VALUES (p_nrodoc, p_nombre, p_apellido);

    COMMIT;
  END insertar;

  PROCEDURE actualizar(
    p_codcliente IN NUMBER,
    p_nrodoc     IN VARCHAR2,
    p_nombre     IN VARCHAR2,
    p_apellido   IN VARCHAR2
  )
  AS
    v_count NUMBER;
  BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM cliente
    WHERE nrodoc = p_nrodoc
      AND codcliente <> p_codcliente;

    IF v_count > 0 THEN
      RAISE_APPLICATION_ERROR(-20010, 'Ya existe un cliente con este documento');
    END IF;

    UPDATE cliente
    SET nombre   = p_nombre,
        apellido = p_apellido,
        nrodoc   = p_nrodoc
    WHERE codcliente = p_codcliente;
  END actualizar;

  PROCEDURE eliminar(
    p_codcliente IN NUMBER
  )
  AS
    v_count NUMBER;
  BEGIN
    SELECT COUNT(*)
    INTO v_count
    FROM cliente
    WHERE codcliente = p_codcliente;

    IF v_count = 0 THEN
      RAISE_APPLICATION_ERROR(-20020, 'No se encontró el cliente');
    END IF;

    SELECT COUNT(*)
    INTO v_count
    FROM venta
    WHERE codcliente = p_codcliente;

    IF v_count > 0 THEN
      RAISE_APPLICATION_ERROR(-20021, 'No se puede eliminar, tiene ventas');
    END IF;

    DELETE FROM cliente
    WHERE codcliente = p_codcliente;
  END eliminar;

END PKG_CLIENTE;
/

GRANT EXECUTE ON PKG_CLIENTE TO example_api;

