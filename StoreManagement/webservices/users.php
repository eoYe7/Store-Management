<?php

$dsn = 'mysql:host=localhost;dbname=users';
$username = 'root';
$password = '';

try {
    $pdo = new PDO($dsn, $username, $password);
	
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);

    if (isset($_POST['login'])) {
        $stmt = $pdo->prepare("SELECT * FROM user WHERE name = :name AND pass = :pass");
        $stmt->bindParam(':name', $_POST['name']);
        $stmt->bindParam(':pass', $_POST['pass']);
        $stmt->execute();

        if ($stmt->rowCount() > 0) {
            echo "logi";
        } else {
            echo "failed";
        }
    }

    if (isset($_POST['showallusers'])) {
        $stmt = $pdo->query("SELECT * FROM user");
        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $data = $row['id'] . "," . $row['name'] . "," . $row['pass'] . "#";
            echo $data;
        }
    }

    // Insert User
    else if (isset($_POST['adduser'])) {
        $stmt = $pdo->prepare("INSERT INTO user (name, pass) VALUES (:name, :pass)");
        $stmt->bindParam(':name', $_POST['name']);
        $stmt->bindParam(':pass', $_POST['pass']);

        if ($stmt->execute()) {
            echo "Inserted successfully";
        } else {
            echo "Insertion failed";
        }
    }

    // Select User
    else if (isset($_POST['selectuser'])) {
        $stmt = $pdo->query("SELECT * FROM user");
        while ($row = $stmt->fetch(PDO::FETCH_ASSOC)) {
            $data = $row['name'] . "," . $row['pass'] . "#";
            echo $data;
        }
    }

    // Update User
    else if (isset($_POST['updateuser'])) {
        $stmt = $pdo->prepare("UPDATE user SET pass = :pass WHERE name = :name");
        $stmt->bindParam(':pass', $_POST['pass']);
        $stmt->bindParam(':name', $_POST['name']);

        if ($stmt->execute()) {
            echo "Updated successfully";
        } else {
            echo "Update failed";
        }
    }

    // Delete User
    else if (isset($_POST['deleteuser'])) {
        $stmt = $pdo->prepare("DELETE FROM user WHERE name = :name AND pass = :pass");
        $stmt->bindParam(':name', $_POST['name']);
        $stmt->bindParam(':pass', $_POST['pass']);

        if ($stmt->execute()) {
            echo "Deleted successfully";
        } else {
            echo "Deletion failed";
        }
    }
} catch (PDOException $e) {
    echo "Connection failed: " . $e->getMessage();
}

?>