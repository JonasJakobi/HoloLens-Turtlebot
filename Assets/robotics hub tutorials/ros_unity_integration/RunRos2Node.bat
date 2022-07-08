docker run -it --rm -p 10000:10000 foxy /bin/bash && export ROS_DOMAIN_ID=9 && ros2 run ros_tcp_endpoint default_server_endpoint --ros-args -p ROS_IP:=0.0.0.0 && pause
