killall dhclient
systemctl stop systemd-networkd.service

echo nameserver 192.168.111.1 > /etc/resolv.conf
echo nameserver 192.168.222.1 >> /etc/resolv.conf

ip link set up dev eth10
ip addr add 192.168.111.2/24 dev eth10
ip link set up dev eth11
ip addr add 192.168.222.2/24 dev eth11

ip route add default via 192.168.111.1

ip link set up dev eth0
ip link set up dev eth1
ip link set up dev eth2
ip link set up dev eth3
ip link set up dev eth4
ip link set up dev eth5
ip link set up dev eth6
ip link set up dev eth7
ip link set up dev eth8
ip link set up dev eth9
ip link set dev eth0 promisc on
ip link set dev eth1 promisc on
ip link set dev eth2 promisc on
ip link set dev eth3 promisc on
ip link set dev eth4 promisc on
ip link set dev eth5 promisc on
ip link set dev eth6 promisc on
ip link set dev eth7 promisc on
ip link set dev eth8 promisc on
ip link set dev eth9 promisc on
ip link set dev eth10 promisc on
ip link set dev eth11 promisc on

#ip link add bond0 type bond
#ip link add bond1 type bond
#ifenslave bond0 eth0 eth1 eth2 eth3 eth8 eth9
#ip addr add 10.1.19.1/16 dev bond0
#ifenslave bond1 eth4 eth5 eth6 eth7
#ip addr add 10.99.19.1/16 dev bond1


brctl addbr br0
ip link set up dev br0
brctl addif br0 eth0
brctl addif br0 eth1 
brctl addif br0 eth2 
brctl addif br0 eth3
brctl stp br0 off
ip addr add 10.99.19.1/16 dev br0
ip link set up dev br0

brctl addbr br1
ip link set up dev br1
brctl addif br1 eth4
brctl addif br1 eth5
brctl addif br1 eth6
brctl addif br1 eth7
brctl addif br0 eth8
brctl addif br0 eth9
brctl stp br1 off
ip addr add 10.1.19.1/16 dev br1
ip link set up dev br1
 nft -f nftable001.list

